using MedAgenda.Application.Dtos.SystemHistory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface ISystemHistoryService
    {
        Task<SystemHistoryDto> CreateHistoryAsync(CreateSystemHistoryDto dto);
        Task<SystemHistoryDto> UpdateHistoryAsync(UpdateSystemHistoryDto dto);
        Task<bool> DeleteHistoryAsync(int id);
        Task<SystemHistoryDto> GetHistoryByIdAsync(int id);
        Task<IEnumerable<SystemHistoryDto>> GetAllHistoriesAsync();
    }
}