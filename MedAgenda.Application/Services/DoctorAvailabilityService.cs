using MedAgenda.Application.Dtos.DoctorAvailability;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces; // ✅ Importa solo esta
using MedAgenda.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _repository; // 🔹 Ahora apunta a Persistence

        public DoctorAvailabilityService(IDoctorAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorAvailabilityDto> CreateAvailabilityAsync(CreateDoctorAvailabilityDto dto)
        {
            if (dto.StartTime >= dto.EndTime)
                throw new System.ArgumentException("La hora de inicio debe ser menor que la de fin");

            var entity = new DoctorAvailability
            {
                ProviderId = dto.ProviderId,
                Day = dto.Day,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _repository.AddAsync(entity);

            return new DoctorAvailabilityDto
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                Day = entity.Day,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }

        public async Task<DoctorAvailabilityDto?> UpdateAvailabilityAsync(UpdateDoctorAvailabilityDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) return null;

            if (dto.StartTime >= dto.EndTime)
                throw new System.ArgumentException("La hora de inicio debe ser menor que la de fin");

            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.ProviderId = dto.ProviderId;
            entity.Day = dto.Day;

            await _repository.UpdateAsync(entity);

            return new DoctorAvailabilityDto
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                Day = entity.Day,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }

        public async Task<bool> DeleteAvailabilityAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<DoctorAvailabilityDto?> GetAvailabilityByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new DoctorAvailabilityDto
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                Day = entity.Day,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }

        public async Task<IEnumerable<DoctorAvailabilityDto>> GetAvailabilitiesByDoctorAsync(int providerId)
        {
            var entities = await _repository.GetAllAsync();
            return entities
                .Where(a => a.ProviderId == providerId)
                .Select(a => new DoctorAvailabilityDto
                {
                    Id = a.Id,
                    ProviderId = a.ProviderId,
                    Day = a.Day,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                });
        }
    }
}