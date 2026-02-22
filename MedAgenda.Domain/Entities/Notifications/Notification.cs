using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities.Notifications
{
    public class Notification : AuditEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
    }
}
