using MedAgenda.Application.Dtos.Specialty;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface IMedicalSpecialtyService
    {
        Task<IEnumerable<MedicalSpecialtyDto>> GetAll();
        Task<MedicalSpecialtyDto> GetById(int id);
        Task Add(CreateMedicalSpecialtyDto dto);
        Task Update(UpdateMedicalSpecialtyDto dto);
        Task Delete(int id);
    }
}