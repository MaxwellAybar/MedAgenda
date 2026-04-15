namespace MedAgenda.WebMVC.Models
{
    public class DoctorAvailabilityDTO
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int Day { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }   
    }
}