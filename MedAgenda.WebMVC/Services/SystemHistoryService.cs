using MedAgenda.WebMVC.Models;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class SystemHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SystemHistoryService> _logger;

        public SystemHistoryService(HttpClient httpClient, ILogger<SystemHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SystemHistoryDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("SystemHistory");
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<SystemHistoryDto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial");
                return new();
            }
        }
    }
}