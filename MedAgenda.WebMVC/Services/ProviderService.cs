using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class ProviderService
    {
        private readonly HttpClient _httpClient;

        public ProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProviderDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProviderDto>>("Provider");
            return response ?? new List<ProviderDto>();
        }

        public async Task<bool> CreateAsync(ProviderDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Provider", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ProviderDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("Provider", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Provider/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}