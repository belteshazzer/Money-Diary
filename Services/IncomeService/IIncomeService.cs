using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.IncomeService
{
    public interface IIncomeService
    {
        Task<Income> GetIncomeAsync(Guid Id); 
        Task<IEnumerable<Income>> GetUserIncomesAsync(string UserId);
        Task<Income> CreateIncomeAsync(IncomeDto income);
        Task<Income> UpdateIncomeAsync(Guid Id, IncomeDto income);
        Task DeleteIncomeAsync(Guid Id);
        Task DeleteAllUserIncomesAsync(string UserId);
    }
}

