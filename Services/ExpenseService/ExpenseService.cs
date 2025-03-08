
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

        public ExpenseService(IGenericRepository<Expense> incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<Expense> CreateExpenseAsync(ExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);

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
    }
}