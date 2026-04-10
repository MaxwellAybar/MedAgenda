using System;

namespace MedAgenda.Application.Dtos.Notification
{
    public class UpdateNotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
    }
}