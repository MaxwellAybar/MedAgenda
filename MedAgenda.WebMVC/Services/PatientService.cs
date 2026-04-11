using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<PatientService> _logger;

        public PatientService(HttpClient httpClient, IConfiguration config, ILogger<PatientService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] + "Patient";
            _logger = logger;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo pacientes");

            var response = await _httpClient.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error: {Error}", error);
                throw new Exception(error);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PatientDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}