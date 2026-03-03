using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;

namespace MedAgenda.Persistence.Repositories
{
    public class ProviderRepository
        : BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(MedAgendaContext context)
            : base(context)
        {
        }
    }
}