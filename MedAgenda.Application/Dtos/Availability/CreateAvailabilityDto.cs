using System;

namespace MedAgenda.Application.Dtos.Availability
{
    public class CreateAvailabilityDto
    {
        public int ProviderId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}