using MedAgenda.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Persistence.Interfaces
{
    public interface ISystemReportsRepository 
    {
        Task<List<SystemReports>> GetAllAsync();
        Task<SystemReports?> GetByIdAsync(int id);
        Task AddAsync(SystemReports entity);
        Task UpdateAsync(SystemReports entity);
        Task DeleteAsync(SystemReports entity);
    }
}