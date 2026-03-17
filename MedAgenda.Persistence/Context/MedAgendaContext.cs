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

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<SystemHistory> SystemHistories { get; set; }

        public DbSet<SystemReports> SystemReports { get; set; }
    }
}