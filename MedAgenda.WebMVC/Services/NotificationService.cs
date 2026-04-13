using MedAgenda.WebMVC.Models;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(HttpClient httpClient, ILogger<NotificationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NotificationDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Notification");
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<NotificationDto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener notificaciones");
                return new();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Notification/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}