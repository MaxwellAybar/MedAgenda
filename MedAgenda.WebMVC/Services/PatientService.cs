using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

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
                return await _httpClient.GetFromJsonAsync<List<PatientDto>>("patient") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de pacientes.");
                return new();
            }
        }

        public async Task<bool> CreateAsync(PatientDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("patient", dto);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("API Error (Patient): {Error}", error);
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fallo de conexión al crear paciente.");
                return false;
            }
        }
    }
}