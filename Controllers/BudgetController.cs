using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyDiary.Common;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.BudgetService;

namespace MoneyDiary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudget(Guid id)
        {
            var budget = await _budgetService.GetBudgetByIdAsync(id);
            return Ok(new ApiResponse<Budget>(System.Net.HttpStatusCode.OK, "Budget retrieved successfully", budget));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBudgets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var budgets = await _budgetService.GetUserBudgetsAsync(userId);
            return Ok(new ApiResponse<IEnumerable<Budget>>(System.Net.HttpStatusCode.OK, "User budgets retrieved successfully", budgets));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetDto budgetDto)
        {
            var createdBudget = await _budgetService.CreateBudgetAsync(budgetDto);
            return CreatedAtAction(nameof(GetBudget), new { id = createdBudget.Id }, new ApiResponse<Budget>(System.Net.HttpStatusCode.OK, "Budget created successfully", createdBudget));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] BudgetDto budgetDto)
        {
            var updatedBudget = await _budgetService.UpdateBudgetAsync(id, budgetDto);
            return Ok(new ApiResponse<Budget>(System.Net.HttpStatusCode.OK, "Budget updated successfully", updatedBudget));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            await _budgetService.DeleteBudgetAsync(id);
            return Ok(new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "Budget deleted successfully", true));
        }
    }
}