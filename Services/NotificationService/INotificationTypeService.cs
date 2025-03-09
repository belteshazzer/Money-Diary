using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.NotificationService
{
    public interface INotificationTypeService
    {
        Task<IEnumerable<NotificationType>> GetAllNotificationTypesAsync();
        Task<NotificationType> GetNotificationTypeByIdAsync(Guid id);
        Task<NotificationType> GetNotificationTypeByTitleAsync(string title);
        Task<NotificationType> CreateNotificationTypeAsync(NotificationTypeDto notificationType);
        Task<NotificationType> UpdateNotificationTypeAsync(Guid id, NotificationTypeDto notificationType);
        Task DeleteNotificationTypeAsync(Guid id);
    }
}