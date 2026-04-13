using MedAgenda.WebMVC.Models;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class ProviderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProviderService> _logger;

        public ProviderService(HttpClient httpClient, ILogger<ProviderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ProviderDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Provider");
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<ProviderDto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener proveedores");
                return new();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Provider/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}