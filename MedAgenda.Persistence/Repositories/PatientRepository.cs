using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging; // <-- 1. Agregar este using

namespace MedAgenda.Persistence.Repositories
{
    public class PatientRepository
        : BaseRepository<Patient>, IPatientRepository
    {
        // 2. Agregar el logger como parámetro del constructor
        public PatientRepository(MedAgendaContext context, ILogger<BaseRepository<Patient>> logger)
            : base(context, logger) // 3. Pasarlo a la base junto con el contexto
        {
        }
    }
}