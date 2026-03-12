using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Application.Dtos.MedicalSpecialty;

namespace MedAgenda.Application.Interfaces
{
    public interface IMedicalSpecialtyService
    {
        Task<List<MedicalSpecialtyDto>> GetAll();

        Task<MedicalSpecialtyDto> GetById(int id);

        Task Add(SaveMedicalSpecialtyDto dto);

        Task Update(UpdateMedicalSpecialtyDto dto);

        Task Delete(int id);
    }
}