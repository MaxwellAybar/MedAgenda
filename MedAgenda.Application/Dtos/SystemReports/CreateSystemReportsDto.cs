using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MedAgenda.Application.Dtos.SystemReports
{
    public class CreateSystemReportDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }



}
