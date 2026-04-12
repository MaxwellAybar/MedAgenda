using System;

namespace MedAgenda.Application.Dtos.Appointment
{
    public class CreateAppointmentDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string Status { get; set; } = "Pendiente";
    }
}