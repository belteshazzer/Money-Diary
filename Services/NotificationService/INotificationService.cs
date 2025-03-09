using MoneyDiary.Models.Entities;

namespace MoneyDiary.Services.NotificationService
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(string userId, int notificationId, Guid? budgetId);
        Task<IEnumerable<Notification>> GetNotificationsAsync(string userId);
        Task<Notification> GetNotificationByIdAsync(Guid id);
        Task SendNotificationAsync(string userId, string notificationTitle, Guid? budgetId);
        Task DeleteNotificationAsync(Guid id);
        Task DeleteAllNotificationsAsync(string userId);
    }
}