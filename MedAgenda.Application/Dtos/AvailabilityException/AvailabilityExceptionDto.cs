using System;

namespace MedAgenda.Application.Dtos.AvailabilityException
{
    public class AvailabilityExceptionDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime ExceptionDate { get; set; }
        public string Reason { get; set; }
    }
}