using System;

namespace MedAgenda.Application.Dtos.Notification
    {
        public class NotificationDto
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Message { get; set; } = null!;
            public DateTime SentDate { get; set; }
            public bool IsRead { get; set; }
        }
    }