using MedAgenda.Application.Dtos.Provider;
using MedAgenda.Application.Exceptions;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repository;
        private readonly ILogger<ProviderService> _logger;

        public ProviderService(
            IProviderRepository repository,
            ILogger<ProviderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProviderDto> CreateProviderAsync(CreateProviderDto dto)
        {
            // INICIO
            _logger.LogInformation("Creando proveedor");

            var entity = new Provider
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            //  ANTES DE GUARDAR
            _logger.LogInformation("Guardando proveedor en base de datos");

            await _repository.AddAsync(entity);

            _logger.LogInformation("Proveedor creado correctamente");

            return new ProviderDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone
            };
        }

        public async Task<ProviderDto> UpdateProviderAsync(UpdateProviderDto dto)
        {
            _logger.LogInformation("Actualizando proveedor con ID: {Id}", dto.Id);

            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Proveedor no encontrado con ID: {Id}", dto.Id);
                throw new NotFoundException("Proveedor no encontrado");
            }

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;

            _logger.LogInformation("Guardando cambios del proveedor");

            await _repository.UpdateAsync(entity);

            _logger.LogInformation("Proveedor actualizado correctamente");

            return new ProviderDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone
            };
        }

        public async Task<bool> DeleteProviderAsync(int id)
        {
            _logger.LogInformation("Eliminando proveedor con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Proveedor no encontrado con ID: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);

            _logger.LogInformation("Proveedor eliminado correctamente");

            return true;
        }

        public async Task<ProviderDto> GetProviderByIdAsync(int id)
        {
            _logger.LogInformation("Obteniendo proveedor con ID: {Id}", id);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Proveedor no encontrado con ID: {Id}", id);
                throw new NotFoundException("Proveedor no encontrado");
            }

            _logger.LogInformation("Proveedor encontrado correctamente");

            return new ProviderDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone
            };
        }

        public async Task<IEnumerable<ProviderDto>> GetAllProvidersAsync()
        {
            _logger.LogInformation("Obteniendo lista de proveedores");

            var data = await _repository.GetAllAsync();

            _logger.LogInformation("Cantidad de proveedores: {Count}", data.Count());

            return data.Select(x => new ProviderDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone
            });
        }
    }
}