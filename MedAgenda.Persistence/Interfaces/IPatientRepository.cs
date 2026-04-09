using MedAgenda.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Persistence.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
    }
}