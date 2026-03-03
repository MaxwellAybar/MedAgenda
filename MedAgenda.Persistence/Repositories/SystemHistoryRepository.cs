using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;

namespace MedAgenda.Persistence.Repositories
{
    public class SystemHistoryRepository
        : BaseRepository<SystemHistory>, ISystemHistoryRepository
    {
        public SystemHistoryRepository(MedAgendaContext context)
            : base(context)
        {
        }
    }
}