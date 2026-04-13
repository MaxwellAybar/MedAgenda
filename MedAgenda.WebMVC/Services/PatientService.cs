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
                return await _httpClient.GetFromJsonAsync<List<PatientDto>>("Patient") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pacientes");
                return new();
            }
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PatientDto>($"Patient/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener paciente por ID");
                return null;
            }
        }

        public async Task<bool> CreateAsync(PatientDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Patient", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(PatientDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"Patient/{dto.Id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Patient/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}