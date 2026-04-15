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
            var entity = new SystemHistory
            {
                UserId = dto.UserId,
                Action = dto.Action,
                Date = DateTime.Now
            };

            await _repository.AddAsync(entity);

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
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                throw new NotFoundException("Registro de historial no encontrado");
            }

            entity.Action = dto.Action;
            await _repository.UpdateAsync(entity);

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
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<SystemHistoryDto> GetHistoryByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
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
            var data = await _repository.GetAllAsync();
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