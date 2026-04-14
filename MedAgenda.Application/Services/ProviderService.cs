using MedAgenda.Application.Dtos.Provider;
using MedAgenda.Application.Interfaces;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Application.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repository;
        private readonly ILogger<ProviderService> _logger;

        public ProviderService(IProviderRepository repository, ILogger<ProviderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProviderDto>> GetAllProvidersAsync()
        {
            _logger.LogInformation("Obteniendo todos los proveedores");
            var data = await _repository.GetAllAsync();
            return data.Select(x => new ProviderDto
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}".Trim(),
                Email = x.Email,
                Phone = x.Phone
            }).ToList();
        }

        public async Task<ProviderDto> GetProviderByIdAsync(int id)
        {
            _logger.LogInformation("Buscando proveedor con ID: {Id}", id);
            var x = await _repository.GetByIdAsync(id);

            if (x == null)
            {
                _logger.LogWarning("Proveedor {Id} no encontrado", id);
                throw new NotFoundException($"El proveedor con ID {id} no existe.");
            }

            return new ProviderDto
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}".Trim(),
                Email = x.Email,
                Phone = x.Phone
            };
        }

        public async Task<ProviderDto> CreateProviderAsync(CreateProviderDto dto)
        {
            _logger.LogInformation("Creando nuevo proveedor: {Name}", dto.Name);
            var parts = dto.Name.Split(' ', 2);
            var entity = new Provider
            {
                FirstName = parts[0],
                LastName = parts.Length > 1 ? parts[1] : string.Empty,
                Email = dto.Email,
                Phone = dto.Phone,
                MedicalSpecialtyId = 1
            };

            await _repository.AddAsync(entity);
            _logger.LogInformation("Proveedor creado con ID: {Id}", entity.Id);

            return new ProviderDto
            {
                Id = entity.Id,
                Name = dto.Name,
                Email = entity.Email,
                Phone = entity.Phone
            };
        }

        public async Task<ProviderDto> UpdateProviderAsync(UpdateProviderDto dto)
        {
            _logger.LogInformation("Actualizando proveedor {Id}", dto.Id);
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Intento de actualizar proveedor inexistente: {Id}", dto.Id);
                throw new NotFoundException("Proveedor no encontrado para actualizar.");
            }

            var parts = dto.Name.Split(' ', 2);
            entity.FirstName = parts[0];
            entity.LastName = parts.Length > 1 ? parts[1] : string.Empty;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;

            await _repository.UpdateAsync(entity);

            return new ProviderDto
            {
                Id = entity.Id,
                Name = dto.Name,
                Email = entity.Email,
                Phone = entity.Phone
            };
        }

        public async Task<bool> DeleteProviderAsync(int id)
        {
            _logger.LogInformation("Eliminando proveedor {Id}", id);
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _logger.LogWarning("Intento de eliminar proveedor inexistente: {Id}", id);
                return false;
            }

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}