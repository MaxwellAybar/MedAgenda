using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Interfaces
{
    public interface IProviderRepository
    {
        Task<List<Provider>> GetAllAsync();
        Task<Provider?> GetByIdAsync(int id);
        Task AddAsync(Provider provider);
        Task UpdateAsync(Provider provider);
        Task DeleteAsync(Provider provider);
    }
}