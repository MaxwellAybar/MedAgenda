using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedAgenda.Persistence.Repositories
{
    public class MedicalSpecialtyRepository
        : BaseRepository<MedicalSpecialty>, IMedicalSpecialtyRepository
    {
        public MedicalSpecialtyRepository(MedAgendaContext context, ILogger<BaseRepository<MedicalSpecialty>> logger)
            : base(context, logger)
        {
        }
    }
}