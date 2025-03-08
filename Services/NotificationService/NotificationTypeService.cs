using AutoMapper;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;

namespace MoneyDiary.Services.NotificationService
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly IGenericRepository<NotificationType> _notificationTypeRepository;
        private readonly IMapper _mapper;

        public NotificationTypeService(IGenericRepository<NotificationType> notificationRepository, IMapper mapper)
        {
            _notificationTypeRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<NotificationType> GetNotificationTypeByIdAsync(Guid id)
        {
           return await _notificationTypeRepository.GetByIdAsync(id);
        }

        public async Task<NotificationType> GetNotificationTypeByTitleAsync(string title)
        {
            var result = await _notificationTypeRepository.GetByConditionAsync(n => n.Title == title);
            var notificationType = result.Where(n => n.Title == title).FirstOrDefault() ?? throw new KeyNotFoundException("Notification type not found.");
            return notificationType;
        }

        public async Task<NotificationType> CreateNotificationTypeAsync(NotificationTypeDto notificationTypeDto)
        {
            var notificationType = _mapper.Map<NotificationType>(notificationTypeDto);
            await _notificationTypeRepository.InsertAsync(notificationType);
            return notificationType;
        }

        public async Task<NotificationType> UpdateNotificationTypeAsync(Guid id, NotificationTypeDto notificationTypeDto)
        {
            var notificationType = await _notificationTypeRepository.GetByIdAsync(id);
            _mapper.Map(notificationTypeDto, notificationType);
            notificationType.UpdatedAt = DateTime.Now;
            await _notificationTypeRepository.UpdateAsync(notificationType);
            return notificationType;
        }

        public async Task DeleteNotificationTypeAsync(Guid id)
        {
            await _notificationTypeRepository.DeleteAsync(id);
        }
    }
}