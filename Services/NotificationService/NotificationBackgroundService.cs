using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneyDiary.Services.BudgetService;
using MoneyDiary.Services.ExpenseService;
using MoneyDiary.Services.AuthService;
using MoneyDiary.Services.UserContextService;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyDiary.Services.NotificationService
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationBackgroundService> _logger;
        private readonly IUserTrackingService _userTrackingService;

        public NotificationBackgroundService(IServiceProvider serviceProvider, ILogger<NotificationBackgroundService> logger, IUserTrackingService userTrackingService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _userTrackingService = userTrackingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NotificationBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("NotificationBackgroundService is checking for notifications.");

                var loggedInUsers = _userTrackingService.GetLoggedInUsers();
                foreach (var userId in loggedInUsers.Keys)
                {
                    _logger.LogInformation($"User ID {userId} found. Checking and sending notifications.");
                    await CheckAndSendNotifications(userId);
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("NotificationBackgroundService is stopping.");
        }

        private async Task CheckAndSendNotifications(string userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var budgetService = scope.ServiceProvider.GetRequiredService<IBudgetService>();
                var expenseService = scope.ServiceProvider.GetRequiredService<IExpenseService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                var NotificationTypeService = scope.ServiceProvider.GetRequiredService<INotificationTypeService>();

                var budgets = await budgetService.GetUserBudgetsAsync(userId);
                var expenses = await expenseService.GetExpensesAsync(userId);
                var notifications = await notificationService.GetNotificationsAsync(userId);
                var notificationTypes = await NotificationTypeService.GetAllNotificationTypesAsync();

                if (budgets != null && expenses != null)
                {
                    foreach (var budget in budgets)
                    {
                        var notificationTypeIds = notificationTypes.Where(x => x.Title == "Expense Exceed Budgeted Balance" || x.Title == "Expense reached Budgeted Balance").Select(x => x.Id).ToList();

                        var notification = notifications.Where(x => x.BudgetId == budget.Id && notificationTypeIds.Contains(x.NotificationTypeId)).FirstOrDefault();

                        if(notification == null)
                        {
                            decimal totalExpense = 0;
                            var localExpenses = expenses.Where(x => x.CreatedAt >= budget.StartDate.ToDateTime(TimeOnly.MinValue) && x.CreatedAt <= budget.EndDate.ToDateTime(TimeOnly.MinValue) && x.CategoryId == budget.CategoryId).ToList();

                            foreach (var expense in localExpenses)
                            {
                                totalExpense += expense.Amount;
                            }

                            if (totalExpense > budget.Amount)
                            {
                                _logger.LogInformation($"User ID {userId}: Expense exceeded budgeted balance for budget ID {budget.Id}.");
                                await notificationService.SendNotificationAsync(userId, "Expense Exceed Budgeted Balance", budget.Id);
                            }
                            else if (totalExpense == budget.Amount)
                            {
                                _logger.LogInformation($"User ID {userId}: Expense reached budgeted balance for budget ID {budget.Id}.");
                                await notificationService.SendNotificationAsync(userId, "Expense reached Budgeted Balance", budget.Id);
                            }
                        }
                    }
                }
                else
                {
                    _logger.LogWarning($"User ID {userId}: No budgets or expenses found.");
                }
            }
        }
    }
}