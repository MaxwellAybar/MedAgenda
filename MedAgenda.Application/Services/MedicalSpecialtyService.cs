using MedAgenda.Application.Dtos.Specialty;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class MedicalSpecialtyService : IMedicalSpecialtyService
    {
        private readonly IMedicalSpecialtyRepository _repository;

        public MedicalSpecialtyService(IMedicalSpecialtyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MedicalSpecialtyDto>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(x => new MedicalSpecialtyDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        }

        public async Task<MedicalSpecialtyDto> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Especialidad no encontrada");

            return new MedicalSpecialtyDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public async Task Add(CreateMedicalSpecialtyDto dto)
        {
            var entity = new MedicalSpecialty
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _repository.AddAsync(entity);
        }

        public async Task Update(UpdateMedicalSpecialtyDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Especialidad no encontrada");

            entity.Name = dto.Name;
            entity.Description = dto.Description;

            await _repository.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Especialidad no encontrada");

            await _repository.DeleteAsync(entity);
        }
    }
}