using MedAgenda.Application.Dtos.Patient;
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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<PatientService> _logger;

        public PatientService(
            IPatientRepository repository,
            ILogger<PatientService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
        {
            _logger.LogInformation("Creando paciente");

            var entity = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth
            };

            _logger.LogInformation("Guardando paciente en base de datos");

            await _repository.AddAsync(entity);

            _logger.LogInformation("Paciente creado correctamente");

            return new PatientDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth
            };
        }

        public async Task<PatientDto> UpdatePatientAsync(UpdatePatientDto dto)
        {
            _logger.LogInformation("Actualizando paciente con ID: {Id}", dto.Id);

            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Paciente no encontrado con ID: {Id}", dto.Id);
                throw new NotFoundException("Paciente no encontrado");
            }

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.DateOfBirth = dto.DateOfBirth;

            _logger.LogInformation("Guardando cambios del paciente");

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Paciente actualizado correctamente");

            return new PatientDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth
            };
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            _logger.LogInformation("Eliminando paciente con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Paciente no encontrado con ID: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);

            _logger.LogInformation("Paciente eliminado correctamente");

            return true;
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo paciente con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Paciente no encontrado con ID: {Id}", id);
                throw new NotFoundException("Paciente no encontrado");
            }

            return new PatientDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth
            };
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            _logger.LogInformation("Obteniendo lista de pacientes");

            var data = await _repository.GetAllAsync();

            _logger.LogInformation("Cantidad de pacientes: {Count}", data.Count());

            return data.Select(x => new PatientDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth
            });
        }
    }
}