using System;

namespace MedAgenda.Application.Dtos.Notification
{
    public class CreateNotificationDto
    {
        public int PatientId { get; set; }
        public string Message { get; set; }
    }
}