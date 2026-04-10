using MedAgenda.Application.Dtos.Notification;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MedAgenda.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto)
        {
            var entity = new Notification
            {
                UserId = dto.UserId,
                Message = dto.Message,
                SentDate = DateTime.Now,
                IsRead = false
            };

            await _repository.AddAsync(entity);

            return new NotificationDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Message = entity.Message,
                SentDate = entity.SentDate,
                IsRead = entity.IsRead
            };
        }

        public async Task<NotificationDto> UpdateNotificationAsync(UpdateNotificationDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Notificación no encontrada");

            entity.Message = dto.Message;
            entity.IsRead = dto.IsRead;

            await _repository.UpdateAsync(entity);

            return new NotificationDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Message = entity.Message,
                SentDate = entity.SentDate,
                IsRead = entity.IsRead
            };
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Notificación no encontrada");

            return new NotificationDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Message = entity.Message,
                SentDate = entity.SentDate,
                IsRead = entity.IsRead
            };
        }

        public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(x => new NotificationDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Message = x.Message,
                SentDate = x.SentDate,
                IsRead = x.IsRead
            });
        }
    }
}