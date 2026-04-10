using MedAgenda.Application.Dtos.DoctorAvailability;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _repository;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorAvailabilityDto> CreateAvailabilityAsync(CreateDoctorAvailabilityDto dto)
        {
            if (dto.StartTime >= dto.EndTime)
                throw new Exception("La hora de inicio debe ser menor que la hora de fin");

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
                throw new Exception("La hora de inicio debe ser menor que la hora de fin");

            entity.Day = dto.Day;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;

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

        public async Task<IEnumerable<DoctorAvailabilityDto>> GetAvailabilitiesByDoctorAsync(int doctorId)
        {
            var data = await _repository.GetByDoctorIdAsync(doctorId);
            return data.Select(x => new DoctorAvailabilityDto
            {
                Id = x.Id,
                ProviderId = x.ProviderId,
                Day = x.Day,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            });
        }
    }
}