using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;

namespace MedAgenda.Domain.Entities.Reports
{
    public class SystemReport : AuditEntity
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}
