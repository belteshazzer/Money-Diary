
using System.Security.Claims;
using AutoMapper;
using Microsoft.Identity.Client;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;
using Newtonsoft.Json;

namespace MoneyDiary.Services.IncomeService
{
    public class IncomeService : IIncomeService
    {
        private readonly IGenericRepository<Income> _incomeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IncomeService(IGenericRepository<Income> incomeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<Income> GetIncomeAsync(Guid Id)
        {
            var income = await _incomeRepository.GetByIdAsync(Id) ?? throw new ArgumentException("Income with the specified Id not found");
            return income;
        }

        public async Task<IEnumerable<Income>> GetUserIncomesAsync(string UserId)
        {
            return await _incomeRepository.GetByConditionAsync(x => x.UserId == UserId);
        }

        public async Task<Income> CreateIncomeAsync(IncomeDto incomeDto)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
            {
                throw new InvalidOperationException("you must be authenticated first to create an income");
            }
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var income = _mapper.Map<Income>(incomeDto);
            income.UserId = userId;
            await _incomeRepository.InsertAsync(income);
            return income;
        }

        public async Task<Income> UpdateIncomeAsync(Guid Id, IncomeDto incomeDto)
        {
            var income = await _incomeRepository.GetByIdAsync(Id);
            var history = _mapper.Map<IncomeHistoryDto>(income);

            var historyList = string.IsNullOrEmpty(income.History) ? [] : JsonConvert.DeserializeObject<List<IncomeHistoryDto>>(income.History);
            historyList.Add(history);

            _mapper.Map(incomeDto, income);
            income.UpdatedAt = DateTime.Now;
            income.History = JsonConvert.SerializeObject(historyList);
            await _incomeRepository.UpdateAsync(income);
            return income;
        }

        public async Task DeleteIncomeAsync(Guid Id)
        {
            var income = await _incomeRepository.GetByIdAsync(Id);
            income.IsDeleted = true;
            await _incomeRepository.UpdateAsync(income);
        }

        public async Task DeleteAllUserIncomesAsync(string UserId)
        {
            var incomes = await _incomeRepository.GetByConditionAsync(x => x.UserId == UserId);
            foreach (var income in incomes)
            {
                await _incomeRepository.DeleteAsync(income);
            }
        }
    }
}