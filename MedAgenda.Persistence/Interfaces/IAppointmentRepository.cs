using MedAgenda.Domain.Entities;
using System.Collections.Generic; 
using System.Threading.Tasks;    

namespace MedAgenda.Persistence.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
        Task<List<Appointment>> GetByPatientIdAsync(int patientId);
        Task<List<Appointment>> GetByDoctorIdAsync(int doctorId);
    }
}