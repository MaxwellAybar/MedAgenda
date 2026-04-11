using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(HttpClient httpClient, IConfiguration config, ILogger<NotificationService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] + "Notification";
            _logger = logger;
        }

        public async Task<List<NotificationDto>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo notificaciones");

            var response = await _httpClient.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error: {Error}", error);
                throw new Exception(error);
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<NotificationDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}