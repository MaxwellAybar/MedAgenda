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
                _logger.LogError(ex, "Error al obtener pacientes");
                return new();
            }
        }

        public async Task<bool> CreateAsync(PatientDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("patient", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear paciente");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(PatientDto dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("patient", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar paciente");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"patient/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar paciente");
                return false;
            }
        }
    }
}