using Microsoft.AspNetCore.Mvc;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.EmailService;
using System.Threading.Tasks;

namespace MoneyDiary.Controllers.EmailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSettingsController : ControllerBase
    {
        private readonly EmailSettingsService _emailSettingsService;

        public EmailSettingsController(EmailSettingsService emailSettingsService)
        {
            _emailSettingsService = emailSettingsService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetEmailSettings()
        {
            var emailSettings = await _emailSettingsService.GetEmailSettingsAsync();
            return Ok(emailSettings);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmailSettings([FromBody] EmailSettings emailSettings)
        {
            await _emailSettingsService.UpdateEmailSettingsAsync(emailSettings);
            return NoContent();
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmailSettings([FromBody] EmailSettings emailSettings)
        {
            await _emailSettingsService.RegisterEmailSettingsAsync(emailSettings);
            return NoContent();
        }
    }
}