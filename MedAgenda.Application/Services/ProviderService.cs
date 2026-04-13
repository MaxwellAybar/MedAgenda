using MedAgenda.Application.Dtos.Provider;
using MedAgenda.Application.Interfaces;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;
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
            try
            {
                var data = await _repository.GetAllAsync();
                return data.Select(x => new ProviderDto
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.LastName}".Trim(),
                    Email = x.Email,
                    Phone = x.Phone
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetAllProvidersAsync");
                return new List<ProviderDto>();
            }
        }

        public async Task<ProviderDto> GetProviderByIdAsync(int id)
        {
            try
            {
                var x = await _repository.GetByIdAsync(id);
                if (x == null) return null!;

                return new ProviderDto
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.LastName}".Trim(),
                    Email = x.Email,
                    Phone = x.Phone
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetProviderByIdAsync {Id}", id);
                return null!;
            }
        }

        public async Task<ProviderDto> CreateProviderAsync(CreateProviderDto dto)
        {
            try
            {
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
                _logger.LogInformation("Provider created {Id}", entity.Id);

                return new ProviderDto { Id = entity.Id, Name = dto.Name, Email = entity.Email, Phone = entity.Phone };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CreateProviderAsync");
                return null!;
            }
        }

        public async Task<ProviderDto> UpdateProviderAsync(UpdateProviderDto dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(dto.Id);
                if (entity == null) return null!;

                var parts = dto.Name.Split(' ', 2);
                entity.FirstName = parts[0];
                entity.LastName = parts.Length > 1 ? parts[1] : string.Empty;
                entity.Email = dto.Email;
                entity.Phone = dto.Phone;

                await _repository.UpdateAsync(entity);
                _logger.LogInformation("Provider updated {Id}", dto.Id);

                return new ProviderDto
                {
                    Id = entity.Id,
                    Name = dto.Name,
                    Email = entity.Email,
                    Phone = entity.Phone
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error UpdateProviderAsync {Id}", dto.Id);
                return null!;
            }
        }

        public async Task<bool> DeleteProviderAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null) return false;

                await _repository.DeleteAsync(entity);
                _logger.LogInformation("Provider deleted {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error DeleteProviderAsync {Id}", id);
                return false;
            }
        }
    }
}