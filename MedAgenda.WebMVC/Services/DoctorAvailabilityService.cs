using MedAgenda.WebMVC.Models;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MedAgenda.WebMVC.Services
{
    public class DoctorAvailabilityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DoctorAvailabilityService> _logger;

        public DoctorAvailabilityService(HttpClient httpClient, ILogger<DoctorAvailabilityService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<DoctorAvailabilityDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("DoctorAvailability");

                if (!response.IsSuccessStatusCode)
                    return new();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DoctorAvailabilityDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener disponibilidades");
                return new();
            }
        }

        public async Task<bool> CreateAsync(DoctorAvailabilityDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("DoctorAvailability", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear disponibilidad");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(DoctorAvailabilityDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("DoctorAvailability", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar disponibilidad");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DoctorAvailability/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar disponibilidad");
                return false;
            }
        }
    }
}