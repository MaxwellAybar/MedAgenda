using MedAgenda.WebMVC.Models;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class SystemReportsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SystemReportsService> _logger;

        public SystemReportsService(HttpClient httpClient, ILogger<SystemReportsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SystemReportsDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("SystemReports");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SystemReportsDto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener reportes");
                return new List<SystemReportsDto>();
            }
        }
    }
}