using MedAgenda.WebMVC.Models;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PatientService> _logger;

        public PatientService(HttpClient httpClient, ILogger<PatientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Patient"); // Solo el nombre del recurso
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PatientDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pacientes");
                return new List<PatientDto>();
            }
        }
    }
}