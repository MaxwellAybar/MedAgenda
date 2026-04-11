using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

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
                _logger.LogInformation("Obteniendo disponibilidades");

               
                var response = await _httpClient.GetAsync("DoctorAvailability");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al obtener: {Error}", error);
                    return new List<DoctorAvailabilityDTO>();
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DoctorAvailabilityDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllAsync");
                return new List<DoctorAvailabilityDTO>();
            }
        }

        public async Task CreateAsync(DoctorAvailabilityDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("DoctorAvailability", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al crear: {Error}", error);
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CreateAsync");
                throw;
            }
        }

        public async Task UpdateAsync(DoctorAvailabilityDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("DoctorAvailability", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al actualizar: {Error}", error);
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateAsync");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
               
                var response = await _httpClient.DeleteAsync($"DoctorAvailability/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al eliminar: {Error}", error);
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteAsync");
                throw;
            }
        }
    }
}