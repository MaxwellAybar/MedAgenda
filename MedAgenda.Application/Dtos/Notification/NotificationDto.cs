using System;

namespace MedAgenda.Application.Dtos.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}