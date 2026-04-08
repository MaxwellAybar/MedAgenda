using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.Extensions.Logging; // <-- Asegúrate de agregar este using

namespace MedAgenda.Persistence.Repositories
{
    public class AppointmentRepository
        : BaseRepository<Appointment>, IAppointmentRepository
    {
        // Agregamos el ILogger al parámetro del constructor
        public AppointmentRepository(MedAgendaContext context, ILogger<BaseRepository<Appointment>> logger)
            : base(context, logger) // Se lo pasamos a la base junto con el contexto
        {
        }
    }
}