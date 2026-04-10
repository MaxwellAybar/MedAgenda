using System.ComponentModel.DataAnnotations;
namespace MedAgenda.WebMVC.Models
{
    public class DoctorAvailabilityDTO
    {
        public int? Id { get; set; }
        public int ProviderId { get; set; }
        public int Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}