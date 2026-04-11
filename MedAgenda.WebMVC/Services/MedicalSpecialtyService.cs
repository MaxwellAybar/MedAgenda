using MedAgenda.WebMVC.Models;
using System.Text.Json;

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
                var response = await _httpClient.GetAsync("MedicalSpecialty");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<MedicalSpecialtyDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener especialidades");
                return new List<MedicalSpecialtyDto>();
            }
        }
    }
}