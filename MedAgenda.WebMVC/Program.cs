using MedAgenda.WebMVC.Services;
using MedAgenda.Persistence.Context;      
using MedAgenda.Persistence.Repositories; 
using MedAgenda.Persistence.Interfaces;   
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MedAgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
builder.Services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();

// Servicios que ya tenías
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<DoctorAvailabilityService>();

var app = builder.Build();

// El resto del código hacia abajo lo dejas exactamente como estaba
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