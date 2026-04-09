using System;

namespace MedAgenda.Application.Dtos.AvailabilityException
{
    public class UpdateAvailabilityExceptionDto
    {
        public int Id { get; set; }
        public DateTime? ExceptionDate { get; set; }
        public string Reason { get; set; }
    }
}