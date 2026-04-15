using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class SystemReportsService
    {
        private readonly HttpClient _httpClient;

        public SystemReportsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SystemReportsDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<SystemReportsDto>>("SystemReports");
            return response ?? new List<SystemReportsDto>();
        }
    }
}