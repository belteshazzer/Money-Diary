
using System.Security.Claims;
using AutoMapper;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;
using Newtonsoft.Json;

namespace MoneyDiary.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly IGenericRepository<Expense> _incomeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ExpenseService> _logger;

        public ExpenseService(IGenericRepository<Expense> incomeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<ExpenseService> logger)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }


        public async Task<Expense> CreateExpenseAsync(ExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            expense.UserId = GetUserId();
            await _incomeRepository.InsertAsync(expense);
            return expense;
        }

        public async Task<Expense> UpdateExpenseAsync(Guid id, ExpenseDto expenseDto)
        {
            var expense = await _incomeRepository.GetByIdAsync(id);

            var history = _mapper.Map<ExpenseHistoryDto>(expense);
            var historyList = string.IsNullOrEmpty(expense.History) ? [] : JsonConvert.DeserializeObject<List<ExpenseHistoryDto>>(expense.History);
            historyList.Add(history);

            _mapper.Map(expenseDto, expense);
            expense.History = JsonConvert.SerializeObject(historyList);
            expense.UpdatedAt = DateTime.Now;

            await _incomeRepository.UpdateAsync(expense);
            return expense;
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            await _incomeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(string userId)
        {
            return await _incomeRepository.GetByConditionAsync(e => e.UserId == userId);
        }

        public async Task<Expense> GetExpenseByIdAsync(Guid id)
        {
            return await _incomeRepository.GetByIdAsync(id);
        }
        public string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("You must be authenticated first to create an expense. Please login and try again.");
                return null;
            }

            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                _logger.LogWarning("Your cookies have been removed. Please login again and try again.");
            }
            else
            {
                _logger.LogInformation($"User ID found: {userId}");
            }

            return userId;
        }
    }
}