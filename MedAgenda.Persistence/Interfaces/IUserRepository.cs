using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(Users user);
    }
}
