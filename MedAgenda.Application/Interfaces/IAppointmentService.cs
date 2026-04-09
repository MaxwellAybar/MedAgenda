using MedAgenda.Application.Dtos.Appointment;

namespace MedAgenda.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> RequestAppointmentAsync(CreateAppointmentDto dto);
        Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto dto);
        Task<bool> CancelAppointmentAsync(int id);
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId);
    }
}