using MedAgenda.WebMVC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace MedAgenda.WebMVC.Services
{
    public class DoctorAvailabilityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<DoctorAvailabilityService> _logger;

        public DoctorAvailabilityService(
            HttpClient httpClient,
            IConfiguration config,
            ILogger<DoctorAvailabilityService> logger)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiSettings:BaseUrl"] + "DoctorAvailability";
            _logger = logger;
        }

       
        public async Task<List<DoctorAvailabilityDTO>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo disponibilidades");

                var response = await _httpClient.GetAsync(_baseUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al obtener: {Error}", error);
                    throw new Exception(error);
                }

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<DoctorAvailabilityDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllAsync");
                throw;
            }
        }

    
        public async Task CreateAsync(DoctorAvailabilityDTO dto)
        {
            try
            {
                _logger.LogInformation("Creando disponibilidad");

                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_baseUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al crear: {Error}", error);
                    throw new Exception(error);
                }

                _logger.LogInformation("Disponibilidad creada correctamente");
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
                _logger.LogInformation("Actualizando disponibilidad con ID: {Id}", dto.Id);

                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(_baseUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al actualizar: {Error}", error);
                    throw new Exception(error);
                }

                _logger.LogInformation("Disponibilidad actualizada correctamente");
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
                _logger.LogInformation("Eliminando disponibilidad con ID: {Id}", id);

                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al eliminar: {Error}", error);
                    throw new Exception(error);
                }

                _logger.LogInformation("Disponibilidad eliminada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteAsync");
                throw;
            }
        }
    }
}