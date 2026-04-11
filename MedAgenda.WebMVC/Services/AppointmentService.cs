using MedAgenda.WebMVC.Models;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient httpClient, ILogger<AppointmentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Appointment");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<AppointmentDto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener citas");
                return new List<AppointmentDto>();
            }
        }
    }
}