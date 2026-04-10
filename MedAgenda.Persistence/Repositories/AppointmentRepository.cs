using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly MedAgendaContext _context;
        private readonly IServiceProvider _serviceProvider; 

        public AppointmentRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<Appointment>> logger,
            IServiceProvider serviceProvider) 
            : base(context, logger)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<List<Appointment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public override async Task AddAsync(Appointment appointment)
        {
            
            var availabilityRepo = (IDoctorAvailabilityRepository)_serviceProvider.GetService(typeof(IDoctorAvailabilityRepository));

            var isAvailable = await availabilityRepo.IsDoctorAvailableAsync(appointment.DoctorId, appointment.AppointmentDate);

            if (!isAvailable)
                throw new Exception("El doctor no está disponible en esa fecha");

            await base.AddAsync(appointment);
        }
    }
}