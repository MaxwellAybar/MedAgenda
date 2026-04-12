using MedAgenda.Application.Dtos.Appointment;
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

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _repository.GetAllAsync();
            return appointments.Select(x => new AppointmentDto
            {
                Id = x.Id,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                AppointmentDate = x.AppointmentDate,
                Status = x.Status ?? "Pendiente",
                Notes = x.Notes ?? string.Empty
            });
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            _logger.LogInformation("Intentando crear cita para Paciente {P} y Doctor {D}", dto.PatientId, dto.DoctorId);


            bool isAvailable = await _availabilityRepository
                .IsDoctorAvailableAsync(dto.DoctorId, dto.AppointmentDate);

            if (!isAvailable)
            {
                throw new NotFoundException("El médico no tiene disponibilidad programada para esta fecha/hora.");
            }

           
            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId, 
                AppointmentDate = dto.AppointmentDate,
                
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pendiente" : dto.Status,
                Notes = dto.Notes ?? "Sin observaciones"
            };

            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError("Error al persistir en la BD: {Msg}", message);
                throw new Exception($"Error de base de datos: {message}");
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

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("Cita no encontrada");

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
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) throw new NotFoundException("Cita no encontrada");

            if (dto.AppointmentDate.HasValue)
            {
                bool isAvailable = await _availabilityRepository
                    .IsDoctorAvailableAsync(entity.DoctorId, dto.AppointmentDate.Value);

                if (!isAvailable) throw new NotFoundException("Médico no disponible en la nueva fecha.");

                entity.AppointmentDate = dto.AppointmentDate.Value;
            }

            entity.Status = dto.Status ?? entity.Status;
            entity.Notes = dto.Notes ?? entity.Notes;

            await _repository.UpdateAsync(entity);

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
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Status = "Cancelada";
            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId)
        {
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