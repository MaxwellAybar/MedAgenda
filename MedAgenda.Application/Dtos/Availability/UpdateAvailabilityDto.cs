using System;

namespace MedAgenda.Application.Dtos.Availability
{
    public class UpdateAvailabilityDto
    {
        public int Id { get; set; }
        public DayOfWeek? Day { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}