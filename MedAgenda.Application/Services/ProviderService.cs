using MedAgenda.Application.Dtos.Provider;
using MedAgenda.Application.Interfaces;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repository;

        public ProviderService(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProviderDto>> GetAllProvidersAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(x => new ProviderDto
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}".Trim()
            });
        }

        public async Task<ProviderDto> GetProviderByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);
            if (x == null) return null;
            return new ProviderDto { Id = x.Id, Name = $"{x.FirstName} {x.LastName}".Trim() };
        }

        public async Task<ProviderDto> CreateProviderAsync(CreateProviderDto dto)
        {
            var names = dto.Name.Split(' ');
            var entity = new Provider
            {
                FirstName = names[0],
                LastName = names.Length > 1 ? string.Join(" ", names.Skip(1)) : string.Empty,
                Email = dto.Email,
                Phone = dto.Phone,
                MedicalSpecialtyId = 1
            };
            await _repository.AddAsync(entity);
            return new ProviderDto { Id = entity.Id, Name = dto.Name };
        }

        public async Task<ProviderDto> UpdateProviderAsync(UpdateProviderDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity != null)
            {
                var names = dto.Name.Split(' ');
                entity.FirstName = names[0];
                entity.LastName = names.Length > 1 ? string.Join(" ", names.Skip(1)) : string.Empty;
                await _repository.UpdateAsync(entity);
            }
            return new ProviderDto { Id = dto.Id, Name = dto.Name };
        }

        public async Task<bool> DeleteProviderAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}