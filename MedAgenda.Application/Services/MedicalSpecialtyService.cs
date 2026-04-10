using MedAgenda.Application.Dtos.Specialty;
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
    public class MedicalSpecialtyService : IMedicalSpecialtyService
    {
        private readonly IMedicalSpecialtyRepository _repository;
        private readonly ILogger<MedicalSpecialtyService> _logger;

        public MedicalSpecialtyService(
            IMedicalSpecialtyRepository repository,
            ILogger<MedicalSpecialtyService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<MedicalSpecialtyDto>> GetAll()
        {
            _logger.LogInformation("Obteniendo lista de especialidades médicas");

            var list = await _repository.GetAllAsync();

            _logger.LogInformation("Cantidad de especialidades: {Count}", list.Count());

            return list.Select(x => new MedicalSpecialtyDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        }

        public async Task<MedicalSpecialtyDto> GetById(int id)
        {
            _logger.LogInformation("Obteniendo especialidad con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Especialidad no encontrada con ID: {Id}", id);
                throw new NotFoundException("Especialidad no encontrada");
            }

            return new MedicalSpecialtyDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public async Task Add(CreateMedicalSpecialtyDto dto)
        {
            _logger.LogInformation("Creando especialidad médica");

            var entity = new MedicalSpecialty
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _logger.LogInformation("Guardando especialidad en base de datos");

            await _repository.AddAsync(entity);

            _logger.LogInformation("Especialidad creada correctamente");
        }

        public async Task Update(UpdateMedicalSpecialtyDto dto)
        {
            _logger.LogInformation("Actualizando especialidad con ID: {Id}", dto.Id);

            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Especialidad no encontrada con ID: {Id}", dto.Id);
                throw new NotFoundException("Especialidad no encontrada");
            }

            entity.Name = dto.Name;
            entity.Description = dto.Description;

            _logger.LogInformation("Guardando cambios de especialidad");

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Especialidad actualizada correctamente");
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation("Eliminando especialidad con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Especialidad no encontrada con ID: {Id}", id);
                throw new NotFoundException("Especialidad no encontrada");
            }

            await _repository.DeleteAsync(entity);

            _logger.LogInformation("Especialidad eliminada correctamente");
        }
    }
}