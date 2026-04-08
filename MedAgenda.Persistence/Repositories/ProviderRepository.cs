using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class ProviderRepository
        : BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<Provider>> logger)
            : base(context, logger)
        {
        }
    }
}