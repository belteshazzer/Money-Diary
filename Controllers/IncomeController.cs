
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyDiary.Common;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.IncomeService;

namespace MoneyDiary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncome(Guid id)
        {
            var income = await _incomeService.GetIncomeAsync(id);

            return Ok(new ApiResponse<Income>(System.Net.HttpStatusCode.OK, "Income retrieved successfully", income));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserIncomes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var incomes = await _incomeService.GetUserIncomesAsync(userId);

            return Ok(new ApiResponse<IEnumerable<Income>>(System.Net.HttpStatusCode.OK, "User incomes retrieved successfully", incomes));
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] IncomeDto income)
        {
            var createdIncome = await _incomeService.CreateIncomeAsync(income);

            return CreatedAtAction(nameof(GetIncome), new { id = createdIncome.Id }, new ApiResponse<Income>(System.Net.HttpStatusCode.OK, "Income created successfully", createdIncome));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(Guid id, [FromBody] IncomeDto income)
        {
            var updatedIncome = await _incomeService.UpdateIncomeAsync(id, income);

            return Ok(new ApiResponse<Income>(System.Net.HttpStatusCode.OK, "Income updated successfully", updatedIncome));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            await _incomeService.DeleteIncomeAsync(id);
            return Ok(new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "Income deleted successfully", true));
        }
    }
}