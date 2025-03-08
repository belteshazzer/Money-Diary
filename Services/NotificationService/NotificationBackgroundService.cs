

using System.Numerics;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.BudgetService;
using MoneyDiary.Services.ExpenseService;
using MoneyDiary.Services.UserContextService;

namespace MoneyDiary.Services.NotificationService
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);
        private readonly IBudgetService _budgetService;
        private readonly IExpenseService _expenseService;
        private readonly IUserContextService _userContextService;

        public NotificationBackgroundService(INotificationService notificationService, IBudgetService budgetService, IExpenseService expenseService, IUserContextService userContextService)
        {
            _notificationService = notificationService;
            _budgetService = budgetService;
            _expenseService = expenseService;
            _userContextService = userContextService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var userId = _userContextService.GetUserId();
                if (userId != null)
                {
                    await CheckAndSendNotifications(userId);

                }
                
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task CheckAndSendNotifications(string userId)
        {
            var budgets = await _budgetService.GetUserBudgetsAsync(userId);
            var expenses = await _expenseService.GetExpensesAsync(userId);

            if (budgets != null && expenses != null)
            {
                foreach (var budget in budgets)
                {
                    decimal totalExpense = 0;
                    var localExpenses = expenses.Where(x => x.CreatedAt >= budget.StartDate.ToDateTime(TimeOnly.MinValue) && x.CreatedAt <= budget.EndDate.ToDateTime(TimeOnly.MinValue) && x.CategoryId == budget.CategoryId).ToList();

                    foreach (var expense in localExpenses)
                    {
                        totalExpense += expense.Amount;
                    }

                    if (totalExpense > budget.Amount)
                    {
                        await _notificationService.SendNotificationAsync(userId,"Expense Exceed Budgeted Balance");
                    }
                    else if (totalExpense == budget.Amount)
                    {
                        await _notificationService.SendNotificationAsync(userId, "Expense reached Budgeted Balance");
                    }
                }
            }
        }
    }
}