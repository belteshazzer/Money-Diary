using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.NotificationService
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(string userId, int notificationId);
        Task<IEnumerable<Notification>> GetNotificationsAsync(string userId);
        Task<Notification> GetNotificationByIdAsync(Guid id);
        Task SendNotificationAsync(string userId, string notificationTitle);
        Task DeleteNotificationAsync(Guid id);
    }
}