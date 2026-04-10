using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class PatientRepository
        : BaseRepository<Patient>, IPatientRepository
    {
        
        public PatientRepository(MedAgendaContext context, ILogger<BaseRepository<Patient>> logger)
            : base(context, logger) 
        {
        }
    }
}