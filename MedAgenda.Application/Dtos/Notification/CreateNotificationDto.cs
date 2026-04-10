using System;

namespace MedAgenda.Application.Dtos.Notification
{
    public class CreateNotificationDto
    {
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
    }
}