using MedAgenda.Application.Dtos.Appointment;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IDoctorAvailabilityRepository _availabilityRepository;

        public AppointmentService(IAppointmentRepository repository, IDoctorAvailabilityRepository availabilityRepository)
        {
            _repository = repository;
            _availabilityRepository = availabilityRepository;
        }

        public async Task<AppointmentDto> RequestAppointmentAsync(CreateAppointmentDto dto)
        {
           
            bool isAvailable = await _availabilityRepository.IsDoctorAvailableAsync(dto.DoctorId, dto.AppointmentDate);
            if (!isAvailable)
                throw new System.Exception("El médico no está disponible en esta fecha/hora");

            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                Status = "Pendiente",
                Notes = dto.Notes
            };

            await _repository.AddAsync(entity);

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
            if (entity == null) throw new System.Exception("Cita no encontrada");

            if (dto.AppointmentDate.HasValue)
            {
                bool isAvailable = await _availabilityRepository.IsDoctorAvailableAsync(entity.DoctorId, dto.AppointmentDate.Value);
                if (!isAvailable)
                    throw new System.Exception("El médico no está disponible en la nueva fecha/hora");
                entity.AppointmentDate = dto.AppointmentDate.Value;
            }

            entity.Status = dto.Status;
            entity.Notes = dto.Notes;

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

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new System.Exception("Cita no encontrada");

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