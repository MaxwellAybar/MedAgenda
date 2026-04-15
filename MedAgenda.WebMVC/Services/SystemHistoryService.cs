using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class SystemHistoryService
    {
        private readonly HttpClient _httpClient;

        public SystemHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SystemHistoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<SystemHistoryDto>>("SystemHistory");
            return response ?? new List<SystemHistoryDto>();
        }
    }
}