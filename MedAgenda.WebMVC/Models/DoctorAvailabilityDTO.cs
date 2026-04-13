using System.ComponentModel.DataAnnotations;

namespace MedAgenda.WebMVC.Models
{
    public class DoctorAvailabilityDTO
    {
        public int? Id { get; set; }

        [Required]
        public int ProviderId { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public string StartTime { get; set; } = string.Empty;

        [Required]
        public string EndTime { get; set; } = string.Empty;
    }
}