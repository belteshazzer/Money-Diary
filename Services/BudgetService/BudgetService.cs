using AutoMapper;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MoneyDiary.Services.BudgetService
{
    public class BudgetService : IBudgetService
    {
        private readonly IGenericRepository<Budget> _budgetRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BudgetService(IGenericRepository<Budget> budgetRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _budgetRepository = budgetRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Budget> GetBudgetByIdAsync(Guid Id)
        {
            return await _budgetRepository.GetByIdAsync(Id);
        }

        public async Task<IEnumerable<Budget>> GetUserBudgetsAsync(string UserId)
        {
            return await _budgetRepository.GetByConditionAsync(x => x.UserId == UserId);
        }

        public async Task<Budget> CreateBudgetAsync(BudgetDto budgetDto)
        {
            var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("you must be authenticated first to create a budget, please login and try again");
            var budget = _mapper.Map<Budget>(budgetDto);
            budget.UserId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("your Cookies have been removed, please login again and try again");
            await _budgetRepository.InsertAsync(budget);
            return budget;
        }

        public async Task<Budget> UpdateBudgetAsync(Guid Id, BudgetDto budgetDto)
        {
            var budget = await _budgetRepository.GetByIdAsync(Id);

            var history = _mapper.Map<BudgetHistoryDto>(budget);
            var historyList = string.IsNullOrEmpty(budget.History) ? [] : JsonConvert.DeserializeObject<List<BudgetHistoryDto>>(budget.History);
            historyList.Add(history);

            _mapper.Map(budgetDto, budget);
            budget.History = JsonConvert.SerializeObject(historyList);
            budget.UpdatedAt = DateTime.UtcNow;
            await _budgetRepository.UpdateAsync(budget);
            return budget;
        }

        public async Task DeleteBudgetAsync(Guid Id)
        {
            await _budgetRepository.DeleteAsync(Id);
        }
    }
}