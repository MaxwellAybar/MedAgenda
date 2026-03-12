using MedAgenda.Application.Dtos.MedicalSpecialty;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;

namespace MedAgenda.Application.Services
{
    public class MedicalSpecialtyService : IMedicalSpecialtyService
    {
        private readonly IMedicalSpecialtyRepository _repository;

        public MedicalSpecialtyService(IMedicalSpecialtyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MedicalSpecialtyDto>> GetAll()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(x => new MedicalSpecialtyDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }

        public async Task<MedicalSpecialtyDto> GetById(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null) return null;

            return new MedicalSpecialtyDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };
        }

        public async Task Add(SaveMedicalSpecialtyDto dto)
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

            if (entity == null) return;

            entity.Name = dto.Name;
            entity.Description = dto.Description;

            await _repository.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return;

            await _repository.DeleteAsync(entity);
        }
    }
}