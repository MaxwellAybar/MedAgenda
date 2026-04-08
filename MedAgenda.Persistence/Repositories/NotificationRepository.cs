using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging; // <-- Agregamos esta librería

namespace MedAgenda.Persistence.Repositories
{
    public class NotificationRepository
        : BaseRepository<Notification>, INotificationRepository
    {
        // El constructor ahora recibe context y logger
        public NotificationRepository(MedAgendaContext context, ILogger<BaseRepository<Notification>> logger)
            : base(context, logger) // Se los pasamos a la base
        {
        }
    }
}