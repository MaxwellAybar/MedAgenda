using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace MedAgenda.WebMVC.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(HttpClient httpClient, ILogger<AppointmentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("appointment");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<AppointmentDto>>() ?? new();

                return new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener citas");
                return new();
            }
        }

        public async Task<bool> CreateAsync(AppointmentDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("appointment", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cita");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(AppointmentDto dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("appointment", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar cita");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"appointment/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar cita");
                return false;
            }
        }
    }
}