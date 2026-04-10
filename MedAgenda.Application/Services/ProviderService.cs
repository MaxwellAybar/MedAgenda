using MedAgenda.Application.Dtos.Provider;
using MedAgenda.Application.Interfaces;
using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MedAgenda.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repository;

        public ProviderService(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProviderDto> CreateProviderAsync(CreateProviderDto dto)
        {
            var entity = new Provider
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            await _repository.AddAsync(entity);

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
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity == null) throw new Exception("Proveedor no encontrado");

            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;

            await _repository.UpdateAsync(entity);

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
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }

        public async Task<ProviderDto> GetProviderByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Proveedor no encontrado");

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
            var data = await _repository.GetAllAsync();
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