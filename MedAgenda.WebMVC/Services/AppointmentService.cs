using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient httpClient, IConfiguration config, ILogger<AppointmentService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] + "Appointment";
            _logger = logger;
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo citas");

            var response = await _httpClient.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error: {Error}", error);
                throw new Exception(error);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<AppointmentDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}