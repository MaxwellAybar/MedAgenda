using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class DoctorAvailabilityService
    {
        private readonly HttpClient _httpClient;

        public DoctorAvailabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DoctorAvailabilityDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<DoctorAvailabilityDTO>>("DoctorAvailability");
            return response ?? new List<DoctorAvailabilityDTO>();
        }

        public async Task<bool> CreateAsync(DoctorAvailabilityDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("DoctorAvailability", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(DoctorAvailabilityDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"DoctorAvailability/{dto.Id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"DoctorAvailability/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}