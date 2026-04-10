using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
   
    public class SystemReportsRepository
        : BaseRepository<SystemReports>, ISystemReportsRepository
    {
        public SystemReportsRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<SystemReports>> logger)
            : base(context, logger)
        {
        }
    }
}