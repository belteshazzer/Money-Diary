using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;

namespace MoneyDiary.Services.NotificationService
{
    public class NotificationService : Hub , INotificationService
    {
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        private readonly INotificationTypeService _notificationTypeService;

        public NotificationService(IGenericRepository<Notification> notificationRepository, IMapper mapper, INotificationTypeService notificationTypeService, IHubContext<NotificationHub> hubContext)  
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _notificationTypeService = notificationTypeService;
            _hubContext = hubContext;
        }

        public async Task DeleteNotificationAsync(Guid id)
        {
            await _notificationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync(string userId)
        {
            return await _notificationRepository.GetByConditionAsync(x => x.UserId == userId);
        }

        public async Task<Notification> GetNotificationByIdAsync(Guid id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        public async Task<Notification> CreateNotificationAsync(string userId, int notificationId)
        {
            var notification = new Notification
            {
                UserId = userId,
                NotificationId = notificationId,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.InsertAsync(notification);
            return notification;
        }

        public async Task SendNotificationAsync(string userId, string notificationTitle)
        {
            var notificationType = await _notificationTypeService.GetNotificationTypeByTitleAsync(notificationTitle);
            var notification = await CreateNotificationAsync(userId, notificationType.Id);

            await _hubContext.Clients.User(userId).SendAsync("RecieveNotification", notification);
        }
    }
}