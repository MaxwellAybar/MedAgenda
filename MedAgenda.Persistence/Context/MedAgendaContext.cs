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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Users>().ToTable("Users");

            modelBuilder.Entity<MedicalSpecialty>().ToTable("MedicalSpecialties");

            modelBuilder.Entity<DoctorAvailability>().ToTable("DoctorAvailabilities");

            modelBuilder.Entity<Appointment>().ToTable("Appointments");

            modelBuilder.Entity<Notification>().ToTable("Notifications");

            modelBuilder.Entity<Patient>().ToTable("Patients");

            modelBuilder.Entity<Provider>().ToTable("Providers");

            modelBuilder.Entity<SystemHistory>().ToTable("SystemHistories");

            modelBuilder.Entity<SystemReports>().ToTable("SystemReports");
        }
    }
}