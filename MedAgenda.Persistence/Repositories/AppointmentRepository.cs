using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAgenda.Persistence.Repositories
{
    public class AppointmentRepository
        : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly MedAgendaContext _context;

        public AppointmentRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<Appointment>> logger)
            : base(context, logger)
        {
            _context = context;
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
    }
}