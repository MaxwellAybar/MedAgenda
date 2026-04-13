using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Services;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Persistence.Repositories;

namespace MedAgenda.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            // --- Repositorios ---
            services.AddScoped<IUserRepository, UserRepository>(); // <--- AGREGAR ESTA
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
            services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();
            services.AddScoped<ISystemReportsRepository, SystemReportsRepository>();

            // --- Servicios ---
            services.AddScoped<IUserService, UserService>();       // <--- AGREGAR ESTA
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMedicalSpecialtyService, MedicalSpecialtyService>();
            services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<ISystemHistoryService, SystemHistoryService>();
            services.AddScoped<ISystemReportsService, SystemReportsService>();

            return services;
        }
    }
}