using MedAgenda.Application.Dtos.SystemReports;
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
    public class SystemReportsService : ISystemReportsService
    {
        private readonly ISystemReportsRepository _repository;
        private readonly ILogger<SystemReportsService> _logger;

        public SystemReportsService(
            ISystemReportsRepository repository,
            ILogger<SystemReportsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<SystemReportsDto> CreateReportAsync(CreateSystemReportsDto dto)
        {
            _logger.LogInformation("Creando reporte del sistema");

            var entity = new SystemReports
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            await _repository.AddAsync(entity);
            _logger.LogInformation("Reporte creado correctamente");

            return new SystemReportsDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Title = entity.Title,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<SystemReportsDto> UpdateReportAsync(UpdateSystemReportsDto dto)
        {
            _logger.LogInformation("Actualizando reporte con ID: {Id}", dto.Id);
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Reporte no encontrado con ID: {Id}", dto.Id);
                throw new NotFoundException("Reporte no encontrado");
            }

            entity.Title = dto.Title;
            entity.Description = dto.Description;

            await _repository.UpdateAsync(entity);
            _logger.LogInformation("Reporte actualizado correctamente");

            return new SystemReportsDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Title = entity.Title,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<bool> DeleteReportAsync(int id)
        {
            _logger.LogInformation("Eliminando reporte con ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Reporte no encontrado con ID: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);
            _logger.LogInformation("Reporte eliminado correctamente");
            return true;
        }

        public async Task<SystemReportsDto> GetReportByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo reporte con ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Reporte no encontrado con ID: {Id}", id);
                throw new NotFoundException("Reporte no encontrado");
            }

            return new SystemReportsDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Title = entity.Title,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate
            };
        }

        public async Task<IEnumerable<SystemReportsDto>> GetAllReportsAsync()
        {
            _logger.LogInformation("Obteniendo todos los reportes");
            var data = await _repository.GetAllAsync();
            _logger.LogInformation("Cantidad de reportes: {Count}", data.Count());

            return data.Select(x => new SystemReportsDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title,
                Description = x.Description,
                CreatedDate = x.CreatedDate
            });
        }
    }
}