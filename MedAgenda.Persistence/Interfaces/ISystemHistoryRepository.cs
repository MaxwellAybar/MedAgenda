using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Interfaces
{
    public interface ISystemHistoryRepository
    {
        Task<List<SystemHistory>> GetAllAsync();
        Task<SystemHistory?> GetByIdAsync(int id);
        Task AddAsync(SystemHistory history);
        Task UpdateAsync(SystemHistory history);
        Task DeleteAsync(SystemHistory history);
    }
}