using Microsoft.AspNetCore.SignalR;

namespace MoneyDiary.Services.NotificationService
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string userId, object message)
        {
            // Send the notification to the specified user
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}