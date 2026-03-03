using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Interfaces
{
    public interface IMedicalSpecialtyRepository
    {
        Task<List<MedicalSpecialty>> GetAllAsync();
        Task<MedicalSpecialty?> GetByIdAsync(int id);
        Task AddAsync(MedicalSpecialty specialty);
        Task UpdateAsync(MedicalSpecialty specialty);
        Task DeleteAsync(MedicalSpecialty specialty);
    }
}
