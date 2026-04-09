using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.DoctorAvailabilities.ToListAsync();
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
            _context.DoctorAvailabilities.Update(availability);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DoctorAvailability availability)
        {
            _context.DoctorAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DoctorAvailability>> GetByProviderAsync(int providerId)
        {
            return await _context.DoctorAvailabilities
                .Where(a => a.ProviderId == providerId)
                .ToListAsync();
        }

        // ✅ ESTE ERA EL QUE FALTABA
        public async Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime appointmentDate)
        {
            return !await _context.DoctorAvailabilities
                .AnyAsync(d => d.ProviderId == doctorId && d.Day == appointmentDate.DayOfWeek);
        }
    }
}