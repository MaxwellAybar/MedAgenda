using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<PatientDto>>("Patient");
            return response ?? new List<PatientDto>();
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PatientDto>($"Patient/{id}");
        }

        public async Task<bool> CreateAsync(PatientDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Patient", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(PatientDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("Patient", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Patient/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}