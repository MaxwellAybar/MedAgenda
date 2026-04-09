using System;

namespace MedAgenda.Application.Dtos.Doctor
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}