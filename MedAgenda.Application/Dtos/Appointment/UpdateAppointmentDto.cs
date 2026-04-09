using System;

namespace MedAgenda.Application.Dtos.Appointment
{
    public class UpdateAppointmentDto
    {
        public int Id { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}