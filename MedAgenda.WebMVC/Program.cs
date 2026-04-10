using MedAgenda.WebMVC.Services;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Repositories;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de la Base de Datos
builder.Services.AddDbContext<MedAgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. REGISTRO ÚNICO DE REPOSITORIOS (Nombres validados)
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
builder.Services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();
builder.Services.AddScoped<ISystemReportsRepository, SystemReportsRepository>(); // Con 's'

// 3. Servicios de Aplicación y Controladores
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<DoctorAvailabilityService>();

var app = builder.Build();

// 4. Configuración del Pipeline de HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();