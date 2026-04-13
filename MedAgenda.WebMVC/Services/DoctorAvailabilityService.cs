using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

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
                return await _httpClient.GetFromJsonAsync<List<DoctorAvailabilityDTO>>("DoctorAvailability") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetAllAsync");
                return new();
            }
        }

        public async Task<bool> CreateAsync(DoctorAvailabilityDTO dto)
        {
            try
            {
                var data = new
                {
                    doctorId = dto.ProviderId,
                    day = dto.Day,
                    startTime = dto.StartTime,
                    endTime = dto.EndTime
                };

                var response = await _httpClient.PostAsJsonAsync("DoctorAvailability", data);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CreateAsync");
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
                _logger.LogError(ex, "Error DeleteAsync");
                return false;
            }
        }
    }
}