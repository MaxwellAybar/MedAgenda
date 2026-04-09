using MedAgenda.Application.Dtos.Patient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
        Task<PatientDto> UpdatePatientAsync(UpdatePatientDto dto);
        Task<bool> DeletePatientAsync(int id);
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    }
}