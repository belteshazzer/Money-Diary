using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task<Expense> CreateExpenseAsync(ExpenseDto expenseDto);
        Task<Expense> UpdateExpenseAsync(Guid id, ExpenseDto expenseDto);
        Task DeleteExpenseAsync(Guid id);
        Task<IEnumerable<Expense>> GetExpensesAsync(string userId);
        Task<Expense> GetExpenseByIdAsync(Guid id);
    }
}