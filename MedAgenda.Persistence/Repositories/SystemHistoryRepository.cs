using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class SystemHistoryRepository
        : BaseRepository<SystemHistory>, ISystemHistoryRepository
    {
        public SystemHistoryRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<SystemHistory>> logger)
            : base(context, logger)
        {
        }
    }
}