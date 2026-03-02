using Microsoft.EntityFrameworkCore;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Persistence.Context
{
    public class MedAgendaContext : DbContext
    {
        public MedAgendaContext(DbContextOptions<MedAgendaContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}