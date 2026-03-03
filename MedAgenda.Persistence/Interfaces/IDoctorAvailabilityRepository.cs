using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Interfaces
{
    public interface IDoctorAvailabilityRepository
    {
        Task<List<DoctorAvailability>> GetAllAsync();
        Task<DoctorAvailability?> GetByIdAsync(int id);
        Task AddAsync(DoctorAvailability availability);
        Task UpdateAsync(DoctorAvailability availability);
        Task DeleteAsync(DoctorAvailability availability);
    }
}