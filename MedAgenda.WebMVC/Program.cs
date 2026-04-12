using MedAgenda.WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


var apiBaseUrl = "https://localhost:7183/api/";


void ConfigureClient(HttpClient client)
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}


builder.Services.AddHttpClient<PatientService>(ConfigureClient);
builder.Services.AddHttpClient<AppointmentService>(ConfigureClient);
builder.Services.AddHttpClient<MedicalSpecialtyService>(ConfigureClient);
builder.Services.AddHttpClient<DoctorAvailabilityService>(ConfigureClient);

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