using MedAgenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Persistence.Interfaces
{
    public interface IDoctorAvailabilityRepository
    {
        Task<List<DoctorAvailability>> GetAllAsync();
        Task<DoctorAvailability?> GetByIdAsync(int id);
        Task AddAsync(DoctorAvailability availability);
        Task UpdateAsync(DoctorAvailability availability);
        Task DeleteAsync(DoctorAvailability availability);

        Task<List<DoctorAvailability>> GetByProviderAsync(int providerId);

        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate);
    }
}