using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class MedicalSpecialtyService
    {
        private readonly HttpClient _httpClient;

        public MedicalSpecialtyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MedicalSpecialtyDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<MedicalSpecialtyDto>>("MedicalSpecialty");
            return response ?? new List<MedicalSpecialtyDto>();
        }

        public async Task<bool> CreateAsync(MedicalSpecialtyDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("MedicalSpecialty", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(MedicalSpecialtyDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("MedicalSpecialty", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"MedicalSpecialty/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}