using System;

namespace MedAgenda.Application.Dtos.DoctorAvailability
{
    public class UpdateDoctorAvailabilityDto
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}