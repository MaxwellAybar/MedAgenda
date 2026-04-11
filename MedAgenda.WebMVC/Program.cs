using MedAgenda.WebMVC.Services;
using MedAgenda.Persistence.Context;
using MedAgenda.Persistence.Repositories;
using MedAgenda.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<MedAgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IMedicalSpecialtyRepository, MedicalSpecialtyRepository>();
builder.Services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();
builder.Services.AddScoped<ISystemReportsRepository, SystemReportsRepository>();


builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient<DoctorAvailabilityService>();
builder.Services.AddHttpClient<PatientService>();
builder.Services.AddHttpClient<AppointmentService>();
builder.Services.AddHttpClient<ProviderService>();
builder.Services.AddHttpClient<MedicalSpecialtyService>();
builder.Services.AddHttpClient<NotificationService>();
builder.Services.AddHttpClient<SystemHistoryService>();
builder.Services.AddHttpClient<SystemReportsService>();

var app = builder.Build();

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