using MedAgenda.Application.Dtos.Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface IProviderService
    {
        Task<ProviderDto> CreateProviderAsync(CreateProviderDto dto);
        Task<ProviderDto> UpdateProviderAsync(UpdateProviderDto dto);
        Task<bool> DeleteProviderAsync(int id);
        Task<ProviderDto> GetProviderByIdAsync(int id);
        Task<IEnumerable<ProviderDto>> GetAllProvidersAsync();
    }
}