using MedAgenda.Application.Dtos.SystemReports;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class SystemReportsService : ISystemReportsService
    {
        private readonly ISystemReportsRepository _repository;

        public SystemReportsService(ISystemReportsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SystemReportsDto> CreateReportAsync(CreateSystemReportsDto dto)
        {
            var entity = new SystemReports // Asegúrate que tu entidad se llame así
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            await _repository.AddAsync(entity);

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
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Reporte no encontrado");

            entity.Title = dto.Title;
            entity.Description = dto.Description;

            await _repository.UpdateAsync(entity);

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
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<SystemReportsDto> GetReportByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

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
            var data = await _repository.GetAllAsync();
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