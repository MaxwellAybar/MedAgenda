using MedAgenda.Application.Exceptions;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Services;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MedAgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});




builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>(); 
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ISystemReportsRepository, SystemReportsRepository>();
builder.Services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();
builder.Services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();


builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMedicalSpecialtyService, MedicalSpecialtyService>();
builder.Services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISystemReportsService, SystemReportsService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISystemHistoryService, SystemHistoryService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features
            .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()
            ?.Error;

        context.Response.ContentType = "application/json";

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(exception.Message);
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Error interno del servidor");
        }
    });
});

app.MapControllers();

app.Run();