namespace MedAgenda.WebMVC.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pendiente";
        public string Notes { get; set; } = string.Empty;
    }
}