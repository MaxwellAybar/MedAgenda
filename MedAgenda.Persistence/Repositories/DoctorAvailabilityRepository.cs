using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedAgenda.Persistence.Repositories
{
    public class DoctorAvailabilityRepository : IDoctorAvailabilityRepository
    {
        private readonly MedAgendaContext _context;

        public DoctorAvailabilityRepository(MedAgendaContext context)
        {
            _context = context;
        }

        
        public async Task<List<DoctorAvailability>> GetAllAsync()
        {
            return await _context.DoctorAvailabilities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DoctorAvailability?> GetByIdAsync(int id)
        {
            return await _context.DoctorAvailabilities.FindAsync(id);
        }

        public async Task AddAsync(DoctorAvailability availability)
        {
            await _context.DoctorAvailabilities.AddAsync(availability);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DoctorAvailability availability)
        {
            _context.Entry(availability).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DoctorAvailability availability)
        {
            _context.DoctorAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DoctorAvailability>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorAvailabilities
                .AsNoTracking()
                .Where(a => a.ProviderId == doctorId)
                .ToListAsync();
        }

        public async Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate)
        {
            
            int dayOfWeek = (int)appointmentDate.DayOfWeek;

            return await _context.DoctorAvailabilities
                .AnyAsync(d => d.ProviderId == doctorId && (int)d.Day == dayOfWeek);
        }
    }
}