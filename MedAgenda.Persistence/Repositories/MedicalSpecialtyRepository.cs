using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging; // <-- 1. Agregamos este using

namespace MedAgenda.Persistence.Repositories
{
    public class MedicalSpecialtyRepository
        : BaseRepository<MedicalSpecialty>, IMedicalSpecialtyRepository
    {
        // 2. Agregamos el ILogger al constructor
        public MedicalSpecialtyRepository(MedAgendaContext context, ILogger<BaseRepository<MedicalSpecialty>> logger)
            : base(context, logger) // 3. Pasamos ambos parámetros a la clase base
        {
        }
    }
}