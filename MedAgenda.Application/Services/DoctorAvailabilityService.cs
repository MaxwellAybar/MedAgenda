using MedAgenda.Application.Dtos.DoctorAvailability;
using MedAgenda.Application.Exceptions;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _repository;
        private readonly ILogger<DoctorAvailabilityService> _logger;

        public DoctorAvailabilityService(
            IDoctorAvailabilityRepository repository,
            ILogger<DoctorAvailabilityService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<DoctorAvailabilityDto> CreateAvailabilityAsync(CreateDoctorAvailabilityDto dto)
        {
            _logger.LogInformation("Creando disponibilidad de doctor");

            if (dto.StartTime >= dto.EndTime)
            {
                _logger.LogWarning("Hora inválida: StartTime >= EndTime");
                throw new NotFoundException("La hora de inicio debe ser menor que la hora de fin");
            }

            var entity = new DoctorAvailability
            {
                ProviderId = dto.ProviderId,
                Day = (DayOfWeek)dto.Day,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _repository.AddAsync(entity);
            _logger.LogInformation("Disponibilidad creada correctamente");

            return new DoctorAvailabilityDto
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                Day = (int)entity.Day, 
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }

        public async Task<DoctorAvailabilityDto?> UpdateAvailabilityAsync(UpdateDoctorAvailabilityDto dto)
        {
            _logger.LogInformation("Actualizando disponibilidad con ID: {Id}", dto.Id);
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Disponibilidad no encontrada con ID: {Id}", dto.Id);
                throw new NotFoundException("Disponibilidad no encontrada");
            }

            if (dto.StartTime >= dto.EndTime)
            {
                _logger.LogWarning("Hora inválida en actualización");
                throw new NotFoundException("La hora de inicio debe ser menor que la hora de fin");
            }

            entity.Day = (DayOfWeek)dto.Day;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.ProviderId = dto.ProviderId;

            await _repository.UpdateAsync(entity);
            _logger.LogInformation("Disponibilidad actualizada correctamente en base de datos");

            return new DoctorAvailabilityDto
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                Day = (int)entity.Day, 
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }

        public async Task<bool> DeleteAvailabilityAsync(int id)
        {
            _logger.LogInformation("Eliminando disponibilidad con ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Disponibilidad no encontrada con ID: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<DoctorAvailabilityDto?> GetAvailabilityByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) throw new NotFoundException("Disponibilidad no encontrada");

            return new DoctorAvailabilityDto
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                Day = (int)entity.Day, 
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
                Day = (int)x.Day, 
                StartTime = x.StartTime,
                EndTime = x.EndTime
            });
        }
    }
}