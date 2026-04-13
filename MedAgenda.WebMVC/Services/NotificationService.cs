using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationService> _logger;
        private readonly JsonSerializerOptions _options;

        public NotificationService(HttpClient httpClient, ILogger<NotificationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<NotificationDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Notification");

                if (!response.IsSuccessStatusCode) return new();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<NotificationDto>>(json, _options) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener notificaciones");
                return new();
            }
        }

        public async Task<bool> CreateAsync(NotificationDto dto)
        {
            try
            {
                
                dto.SentDate = DateTime.Now;

                var response = await _httpClient.PostAsJsonAsync("Notification", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear notificación");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Notification/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar notificación");
                return false;
            }
        }
    }
}