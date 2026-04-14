using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class DoctorAvailabilityRepository : IDoctorAvailabilityRepository
    {
        private readonly MedAgendaContext _context;
        private readonly ILogger<DoctorAvailabilityRepository> _logger;

        public DoctorAvailabilityRepository(MedAgendaContext context, ILogger<DoctorAvailabilityRepository> logger)
        {
            _context = context;
            _logger = logger;
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
            try
            {
                await _context.DoctorAvailabilities.AddAsync(availability);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Operación AddAsync exitosa");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en AddAsync");
                throw;
            }
        }

        public async Task UpdateAsync(DoctorAvailability availability)
        {
            try
            {
                _context.Entry(availability).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Operación UpdateAsync exitosa");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateAsync");
                throw;
            }
        }

        public async Task DeleteAsync(DoctorAvailability availability)
        {
            try
            {
                _context.DoctorAvailabilities.Remove(availability);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Operación DeleteAsync exitosa");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteAsync");
                throw;
            }
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