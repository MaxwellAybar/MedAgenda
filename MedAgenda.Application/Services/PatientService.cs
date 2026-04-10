using MedAgenda.Application.Dtos.Patient;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
        {
            var entity = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth
            };

            await _repository.AddAsync(entity);

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
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
                throw new System.Exception("Paciente no encontrado");

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.DateOfBirth = dto.DateOfBirth;

            await _repository.UpdateAsync(entity);

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
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new System.Exception("Paciente no encontrado");

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
            var data = await _repository.GetAllAsync();

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