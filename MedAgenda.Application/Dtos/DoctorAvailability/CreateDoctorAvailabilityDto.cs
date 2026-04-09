using System;

namespace MedAgenda.Application.Dtos.DoctorAvailability
{
    public class CreateDoctorAvailabilityDto
    {
        public int ProviderId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}