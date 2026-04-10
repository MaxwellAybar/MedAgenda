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

        Task<List<DoctorAvailability>> GetByDoctorIdAsync(int doctorId);

        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate);
    }
}