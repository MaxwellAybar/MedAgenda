using MedAgenda.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Persistence.Repositories;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// ? Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Configuración de DbContext
builder.Services.AddDbContext<MedAgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ISystemReportsRepository, SystemReportsRepository>();
builder.Services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();
builder.Services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>(); // ?? Persistence repo

// ? Servicios
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMedicalSpecialtyService, MedicalSpecialtyService>();
builder.Services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>(); // ?? Servicio correcto
builder.Services.AddScoped<IPatientService, PatientService>();

var app = builder.Build();

// ? Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ? CORS
app.UseCors("AllowAll");

// ? Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// ? Mapear controladores
app.MapControllers();

// ? Ejecutar aplicación
app.Run();