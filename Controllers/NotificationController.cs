using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyDiary.Common;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.NotificationService;

namespace MoneyDiary.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationController(INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
        {
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = await _notificationService.GetNotificationsAsync(userId);
            return Ok(new ApiResponse<IEnumerable<Notification>>(System.Net.HttpStatusCode.OK, "Notifications retrieved successfully", notifications));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllNotifications()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _notificationService.DeleteAllNotificationsAsync(userId);
            return Ok(new ApiResponse<string>(System.Net.HttpStatusCode.OK, "Notifications deleted successfully", null));
        }

    }
}