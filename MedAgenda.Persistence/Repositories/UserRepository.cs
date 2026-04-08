using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<Users>> logger)
            : base(context, logger)
        {
        }
    }
}