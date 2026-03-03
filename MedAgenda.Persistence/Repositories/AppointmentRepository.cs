using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;

namespace MedAgenda.Persistence.Repositories
{
    public class AppointmentRepository
        : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MedAgendaContext context)
            : base(context)
        {
        }
    }
}