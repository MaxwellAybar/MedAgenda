using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class ProviderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<ProviderService> _logger;

        public ProviderService(HttpClient httpClient, IConfiguration config, ILogger<ProviderService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] + "Provider";
            _logger = logger;
        }

        public async Task<List<ProviderDto>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo proveedores");

            var response = await _httpClient.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error: {Error}", error);
                throw new Exception(error);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProviderDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}