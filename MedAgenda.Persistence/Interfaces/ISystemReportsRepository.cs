using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Interfaces
{
    public interface ISystemReportsRepository
    {
        Task<List<SystemReports>> GetAllAsync();
        Task<SystemReports?> GetByIdAsync(int id);
        Task AddAsync(SystemReports report);
        Task UpdateAsync(SystemReports report);
        Task DeleteAsync(SystemReports report);
    }
}