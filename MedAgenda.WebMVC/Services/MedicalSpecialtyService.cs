using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class MedicalSpecialtyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<MedicalSpecialtyService> _logger;

        public MedicalSpecialtyService(HttpClient httpClient, IConfiguration config, ILogger<MedicalSpecialtyService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] + "MedicalSpecialty";
            _logger = logger;
        }

        public async Task<List<MedicalSpecialtyDto>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo especialidades");

            var response = await _httpClient.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error: {Error}", error);
                throw new Exception(error);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<MedicalSpecialtyDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}