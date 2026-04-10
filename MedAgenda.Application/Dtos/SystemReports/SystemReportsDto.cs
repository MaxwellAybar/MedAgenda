using System;

namespace MedAgenda.Application.Dtos.SystemReports
{
    public class SystemReportsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CreateSystemReportsDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSystemReportsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}