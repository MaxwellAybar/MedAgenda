using System;

namespace MedAgenda.Application.Dtos.DoctorAvailability
{
    public class DoctorAvailabilityDto
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}