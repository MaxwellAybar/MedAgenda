using System;

namespace MedAgenda.Application.Dtos.Specialty
{
    public class UpdateMedicalSpecialtyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}