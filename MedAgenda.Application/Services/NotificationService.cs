using MedAgenda.Application.Dtos.Notification;
using MedAgenda.Application.Exceptions;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository repository,
            ILogger<NotificationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto)
        {
            _logger.LogInformation("Creando notificación");

            var entity = new Notification
            {
                UserId = dto.UserId,
                Message = dto.Message,
                SentDate = DateTime.Now,
                IsRead = false
            };

            _logger.LogInformation("Guardando notificación en base de datos");

            await _repository.AddAsync(entity);

            _logger.LogInformation("Notificación creada correctamente");

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
            _logger.LogInformation("Actualizando notificación con ID: {Id}", dto.Id);

            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Notificación no encontrada con ID: {Id}", dto.Id);
                throw new NotFoundException("Notificación no encontrada");
            }

            entity.Message = dto.Message;
            entity.IsRead = dto.IsRead;

            _logger.LogInformation("Guardando cambios de notificación");

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Notificación actualizada correctamente");

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
            _logger.LogInformation("Eliminando notificación con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Notificación no encontrada con ID: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);

            _logger.LogInformation("Notificación eliminada correctamente");

            return true;
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo notificación con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Notificación no encontrada con ID: {Id}", id);
                throw new NotFoundException("Notificación no encontrada");
            }

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
            _logger.LogInformation("Obteniendo todas las notificaciones");

            var data = await _repository.GetAllAsync();

            _logger.LogInformation("Cantidad de notificaciones: {Count}", data.Count());

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