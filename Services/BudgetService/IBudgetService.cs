using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.BudgetService
{
    public interface IBudgetService
    {
        Task<Budget> CreateBudgetAsync(BudgetDto budgetDto);
        Task<Budget> UpdateBudgetAsync(Guid id, BudgetDto budgetDto);
        Task DeleteBudgetAsync(Guid id);
        Task<IEnumerable<Budget>> GetUserBudgetsAsync(string userId);
        Task<Budget> GetBudgetByIdAsync(Guid id);
    }
}