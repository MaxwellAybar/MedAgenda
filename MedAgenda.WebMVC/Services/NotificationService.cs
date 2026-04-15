using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NotificationDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NotificationDto>>("Notification");
            return response ?? new List<NotificationDto>();
        }

        public async Task<bool> CreateAsync(NotificationDto dto)
        {
            dto.SentDate = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync("Notification", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Notification/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}