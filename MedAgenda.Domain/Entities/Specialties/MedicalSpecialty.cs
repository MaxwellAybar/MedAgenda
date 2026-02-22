using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities.Specialties
{
    public class MedicalSpecialty : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
