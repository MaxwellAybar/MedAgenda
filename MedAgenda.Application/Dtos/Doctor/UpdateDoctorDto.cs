using System;

namespace MedAgenda.Application.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SpecialtyId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}