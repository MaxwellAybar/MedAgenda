using MedAgenda.Application.Dtos.Appointment;
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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IDoctorAvailabilityRepository _availabilityRepository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(
            IAppointmentRepository repository,
            IDoctorAvailabilityRepository availabilityRepository,
            ILogger<AppointmentService> logger)
        {
            _repository = repository;
            _availabilityRepository = availabilityRepository;
            _logger = logger;
        }

        public async Task<AppointmentDto> RequestAppointmentAsync(CreateAppointmentDto dto)
        {
            _logger.LogInformation("Solicitando cita médica");

            bool isAvailable = await _availabilityRepository
                .IsDoctorAvailableAsync(dto.DoctorId, dto.AppointmentDate);

            if (!isAvailable)
            {
                _logger.LogWarning("Doctor no disponible para la fecha solicitada");
                throw new NotFoundException("El médico no está disponible en esta fecha/hora");
            }

            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                Status = "Pendiente",
                Notes = dto.Notes
            };

            _logger.LogInformation("Guardando cita en base de datos");

            await _repository.AddAsync(entity);

            _logger.LogInformation("Cita creada correctamente");

            return new AppointmentDto
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                AppointmentDate = entity.AppointmentDate,
                Status = entity.Status,
                Notes = entity.Notes
            };
        }

        public async Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto dto)
        {
            _logger.LogInformation("Actualizando cita con ID: {Id}", dto.Id);

            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Cita no encontrada con ID: {Id}", dto.Id);
                throw new NotFoundException("Cita no encontrada");
            }

            if (dto.AppointmentDate.HasValue)
            {
                bool isAvailable = await _availabilityRepository
                    .IsDoctorAvailableAsync(entity.DoctorId, dto.AppointmentDate.Value);

                if (!isAvailable)
                {
                    _logger.LogWarning("Doctor no disponible para nueva fecha");
                    throw new NotFoundException("El médico no está disponible en la nueva fecha/hora");
                }

                entity.AppointmentDate = dto.AppointmentDate.Value;
            }

            entity.Status = dto.Status;
            entity.Notes = dto.Notes;

            _logger.LogInformation("Guardando cambios de la cita");

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Cita actualizada correctamente");

            return new AppointmentDto
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                AppointmentDate = entity.AppointmentDate,
                Status = entity.Status,
                Notes = entity.Notes
            };
        }

        public async Task<bool> CancelAppointmentAsync(int id)
        {
            _logger.LogInformation("Cancelando cita con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Cita no encontrada con ID: {Id}", id);
                return false;
            }

            entity.Status = "Cancelada";

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Cita cancelada correctamente");

            return true;
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo cita con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Cita no encontrada con ID: {Id}", id);
                throw new NotFoundException("Cita no encontrada");
            }

            return new AppointmentDto
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                AppointmentDate = entity.AppointmentDate,
                Status = entity.Status,
                Notes = entity.Notes
            };
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId)
        {
            _logger.LogInformation("Obteniendo citas por paciente: {PatientId}", patientId);

            var data = await _repository.GetByPatientIdAsync(patientId);

            return data.Select(x => new AppointmentDto
            {
                Id = x.Id,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                AppointmentDate = x.AppointmentDate,
                Status = x.Status,
                Notes = x.Notes
            });
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            _logger.LogInformation("Obteniendo citas por doctor: {DoctorId}", doctorId);

            var data = await _repository.GetByDoctorIdAsync(doctorId);

            return data.Select(x => new AppointmentDto
            {
                Id = x.Id,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                AppointmentDate = x.AppointmentDate,
                Status = x.Status,
                Notes = x.Notes
            });
        }
    }
}