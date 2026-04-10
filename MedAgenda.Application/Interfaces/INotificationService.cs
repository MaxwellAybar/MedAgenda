using MedAgenda.Application.Dtos.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto);
        Task<NotificationDto> UpdateNotificationAsync(UpdateNotificationDto dto);
        Task<bool> DeleteNotificationAsync(int id);
        Task<NotificationDto> GetNotificationByIdAsync(int id);
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync();
    }
}