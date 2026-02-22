using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities.History
{
    public class SystemHistory : AuditEntity
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
