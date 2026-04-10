using MedAgenda.Application.Dtos.SystemReports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface ISystemReportsService
    {
        Task<SystemReportsDto> CreateReportAsync(CreateSystemReportsDto dto);
        Task<SystemReportsDto> UpdateReportAsync(UpdateSystemReportsDto dto);
        Task<bool> DeleteReportAsync(int id);
        Task<SystemReportsDto> GetReportByIdAsync(int id);
        Task<IEnumerable<SystemReportsDto>> GetAllReportsAsync();
    }
}