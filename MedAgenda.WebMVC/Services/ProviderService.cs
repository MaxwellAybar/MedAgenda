using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

namespace MedAgenda.WebMVC.Services
{
    public class ProviderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProviderService> _logger;

        public ProviderService(HttpClient httpClient, ILogger<ProviderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ProviderDto>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ProviderDto>>("Provider") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener proveedores");
                return new();
            }
        }

        public async Task<bool> CreateAsync(ProviderDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Provider", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear proveedor");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ProviderDto dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Provider/{dto.Id}", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar proveedor");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Provider/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar proveedor");
                return false;
            }
        }
    }
}