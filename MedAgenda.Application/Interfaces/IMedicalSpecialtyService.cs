using MedAgenda.Application.Dtos.Specialty;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface IMedicalSpecialtyService
    {
        Task<List<MedicalSpecialtyDto>> GetAll();

        Task<MedicalSpecialtyDto> GetById(int id);

        Task Add(CreateMedicalSpecialtyDto dto); // <- Cambiado a CreateMedicalSpecialtyDto

        Task Update(UpdateMedicalSpecialtyDto dto);

        Task Delete(int id);
    }
}