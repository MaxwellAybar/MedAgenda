using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities
{
    public class MedicalSpecialty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}