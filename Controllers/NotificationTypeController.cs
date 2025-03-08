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
    public class NotificationTypeController : ControllerBase
    {
        private readonly INotificationTypeService _notificationTypeService;

        public NotificationTypeController(INotificationTypeService notificationTypeService)
        {
            _notificationTypeService = notificationTypeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationType(Guid id)
        {
            var notificationType = await _notificationTypeService.GetNotificationTypeByIdAsync(id);
            return Ok(new ApiResponse<NotificationType>(System.Net.HttpStatusCode.OK, "Notification type retrieved successfully", notificationType));
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetNotificationTypeByTitle(string title)
        {
            var notificationType = await _notificationTypeService.GetNotificationTypeByTitleAsync(title);
            return Ok(new ApiResponse<NotificationType>(System.Net.HttpStatusCode.OK, "Notification type retrieved successfully", notificationType));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotificationType([FromBody] NotificationTypeDto notificationTypeDto)
        {
            var createdNotificationType = await _notificationTypeService.CreateNotificationTypeAsync(notificationTypeDto);
            return CreatedAtAction(nameof(GetNotificationType), new { id = createdNotificationType.Id }, new ApiResponse<NotificationType>(System.Net.HttpStatusCode.OK, "Notification type created successfully", createdNotificationType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationType(Guid id, [FromBody] NotificationTypeDto notificationTypeDto)
        {
            var updatedNotificationType = await _notificationTypeService.UpdateNotificationTypeAsync(id, notificationTypeDto);
            return Ok(new ApiResponse<NotificationType>(System.Net.HttpStatusCode.OK, "Notification type updated successfully", updatedNotificationType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationType(Guid id)
        {
            await _notificationTypeService.DeleteNotificationTypeAsync(id);
            return Ok(new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "Notification type deleted successfully", true));
        }
    }
}