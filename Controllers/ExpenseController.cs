using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyDiary.Common;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.ExpenseService;

namespace MoneyDiary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenseService.GetExpensesAsync(userId);
            return Ok(new ApiResponse<IEnumerable<Expense>>(System.Net.HttpStatusCode.OK, "Expenses retrieved successfully", expenses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            return Ok(new ApiResponse<Expense>(System.Net.HttpStatusCode.OK, "Expense retrieved successfully", expense));
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseDto expenseDto)
        {
            var createdExpense = await _expenseService.CreateExpenseAsync(expenseDto);
            return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, new ApiResponse<Expense>(System.Net.HttpStatusCode.OK, "Expense created successfully", createdExpense));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] ExpenseDto expenseDto)
        {
            var updatedExpense = await _expenseService.UpdateExpenseAsync(id, expenseDto);
            return Ok(new ApiResponse<Expense>(System.Net.HttpStatusCode.OK, "Expense updated successfully", updatedExpense));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            await _expenseService.DeleteExpenseAsync(id);
            return Ok(new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "Expense deleted successfully", true));
        }
    }
}