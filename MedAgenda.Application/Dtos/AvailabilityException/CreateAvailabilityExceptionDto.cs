using System;

namespace MedAgenda.Application.Dtos.AvailabilityException
{
    public class CreateAvailabilityExceptionDto
    {
        public int DoctorId { get; set; }
        public DateTime ExceptionDate { get; set; }
        public string Reason { get; set; }
    }
}