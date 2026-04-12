using MedAgenda.Application.Dtos.Appointment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface IAppointmentService
    {

        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();


        Task<AppointmentDto> GetAppointmentByIdAsync(int id);

  
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto);

     
        Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto dto);

    
        Task<bool> CancelAppointmentAsync(int id);

      
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId);
    }
}