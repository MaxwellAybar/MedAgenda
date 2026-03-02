using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Base;



namespace MedAgenda.Domain.Entities
{
    public class SystemReports
    {
        public int Id { get; set; }

        public string ReportName { get; set; }

        public DateTime GeneratedDate { get; set; }

        public string GeneratedBy { get; set; }
    }
}