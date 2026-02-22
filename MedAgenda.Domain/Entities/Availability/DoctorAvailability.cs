using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities.Availability
{
    public class DoctorAvailability : AuditEntity
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime AvailableDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
