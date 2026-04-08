using MedAgenda.WebMVC.Models;
using System.Text;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class DoctorAvailabilityService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "https://localhost:7183/api/DoctorAvailability";

        public DoctorAvailabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DoctorAvailabilityDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(baseUrl);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DoctorAvailabilityDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task CreateAsync(DoctorAvailabilityDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(baseUrl, content);
        }

        public async Task UpdateAsync(DoctorAvailabilityDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(baseUrl, content);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{baseUrl}/{id}");
        }
    }
}