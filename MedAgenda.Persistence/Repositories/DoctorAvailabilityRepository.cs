using MedAgenda.Domain.Entities;
using MedAgenda.Persistence.Base;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedAgenda.Persistence.Repositories
{
    public class DoctorAvailabilityRepository
        : BaseRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        public DoctorAvailabilityRepository(
            MedAgendaContext context,
            ILogger<BaseRepository<DoctorAvailability>> logger)
            : base(context, logger)
        {
        }

        // 🔥 IMPLEMENTACIÓN REAL (LO QUE EL PROFE QUIERE)
        public async Task<List<DoctorAvailability>> GetByProviderAsync(int providerId)
        {
            try
            {
                return await _context.DoctorAvailabilities
                    .Where(x => x.ProviderId == providerId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener disponibilidades por provider");
                throw;
            }
        }
    }
}