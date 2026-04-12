using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedAgenda.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

      
        [Column("ProviderId")]
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public string Status { get; set; } = "Pendiente";

        public string Notes { get; set; } = string.Empty;
    }
}