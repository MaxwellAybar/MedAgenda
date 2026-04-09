using System;

namespace MedAgenda.Application.Dtos.Availability
{
    public class AvailabilityDto
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}