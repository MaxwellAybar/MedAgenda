using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging; 

namespace MedAgenda.Persistence.Repositories
{
    public class NotificationRepository
        : BaseRepository<Notification>, INotificationRepository
    {
        
        public NotificationRepository(MedAgendaContext context, ILogger<BaseRepository<Notification>> logger)
            : base(context, logger) 
        {
        }
    }
}