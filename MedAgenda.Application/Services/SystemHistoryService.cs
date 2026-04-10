using MedAgenda.Application.Dtos.SystemHistory;
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
    public class SystemHistoryService : ISystemHistoryService
    {
        private readonly ISystemHistoryRepository _repository;
        private readonly ILogger<SystemHistoryService> _logger;

        public SystemHistoryService(
            ISystemHistoryRepository repository,
            ILogger<SystemHistoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<SystemHistoryDto> CreateHistoryAsync(CreateSystemHistoryDto dto)
        {
            _logger.LogInformation("Creando registro de historial");

            var entity = new SystemHistory
            {
                UserId = dto.UserId,
                Action = dto.Action,
                Date = DateTime.Now
            };

            _logger.LogInformation("Guardando historial en base de datos");

            await _repository.AddAsync(entity);

            _logger.LogInformation("Historial creado correctamente");

            return new SystemHistoryDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Action = entity.Action,
                Date = entity.Date
            };
        }

        public async Task<SystemHistoryDto> UpdateHistoryAsync(UpdateSystemHistoryDto dto)
        {
            _logger.LogInformation("Actualizando historial con ID: {Id}", dto.Id);

            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Historial no encontrado con ID: {Id}", dto.Id);
                throw new NotFoundException("Registro de historial no encontrado");
            }

            entity.Action = dto.Action;

            _logger.LogInformation("Guardando cambios del historial");

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Historial actualizado correctamente");

            return new SystemHistoryDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Action = entity.Action,
                Date = entity.Date
            };
        }

        public async Task<bool> DeleteHistoryAsync(int id)
        {
            _logger.LogInformation("Eliminando historial con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Historial no encontrado con ID: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);

            _logger.LogInformation("Historial eliminado correctamente");

            return true;
        }

        public async Task<SystemHistoryDto> GetHistoryByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo historial con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Historial no encontrado con ID: {Id}", id);
                throw new NotFoundException("Registro de historial no encontrado");
            }

            return new SystemHistoryDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Action = entity.Action,
                Date = entity.Date
            };
        }

        public async Task<IEnumerable<SystemHistoryDto>> GetAllHistoriesAsync()
        {
            _logger.LogInformation("Obteniendo todos los historiales");

            var data = await _repository.GetAllAsync();

            _logger.LogInformation("Cantidad de historiales: {Count}", data.Count());

            return data.Select(x => new SystemHistoryDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Action = x.Action,
                Date = x.Date
            });
        }
    }
}