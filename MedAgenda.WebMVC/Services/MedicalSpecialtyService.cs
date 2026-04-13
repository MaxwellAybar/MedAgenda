using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class MedicalSpecialtyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalSpecialtyService> _logger;

        public MedicalSpecialtyService(HttpClient httpClient, ILogger<MedicalSpecialtyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalSpecialtyDto>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MedicalSpecialtyDto>>("MedicalSpecialty") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener especialidades");
                return new();
            }
        }

        public async Task<bool> CreateAsync(MedicalSpecialtyDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("MedicalSpecialty", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(MedicalSpecialtyDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("MedicalSpecialty", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"MedicalSpecialty/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}