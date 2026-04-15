using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AppointmentDto>>("appointment");
            return response ?? new List<AppointmentDto>();
        }

        public async Task<bool> CreateAsync(AppointmentDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("appointment", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(AppointmentDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("appointment", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"appointment/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}