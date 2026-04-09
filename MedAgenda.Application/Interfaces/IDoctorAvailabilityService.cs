using MedAgenda.Application.Dtos.Availability;
using MedAgenda.Application.Dtos.DoctorAvailability;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Application.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        Task<DoctorAvailabilityDto> CreateAvailabilityAsync(CreateDoctorAvailabilityDto dto);
        Task<DoctorAvailabilityDto?> UpdateAvailabilityAsync(UpdateDoctorAvailabilityDto dto);
        Task<bool> DeleteAvailabilityAsync(int id);
        Task<DoctorAvailabilityDto?> GetAvailabilityByIdAsync(int id);
        Task<IEnumerable<DoctorAvailabilityDto>> GetAvailabilitiesByDoctorAsync(int doctorId);
    }
}