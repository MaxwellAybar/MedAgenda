using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities
{
    public class SystemHistory
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public string PerformedBy { get; set; }

        public DateTime Date { get; set; }
    }
}