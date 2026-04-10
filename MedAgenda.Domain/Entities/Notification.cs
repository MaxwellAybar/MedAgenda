using System;

namespace MedAgenda.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }      
        public string Message { get; set; }
        public DateTime SentDate { get; set; } 
        public bool IsRead { get; set; }
    }
}