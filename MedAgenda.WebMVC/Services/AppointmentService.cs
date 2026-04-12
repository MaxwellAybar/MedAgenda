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
                _logger.LogInformation("Solicitando lista de citas al API.");
                var response = await _httpClient.GetAsync("appointment");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<AppointmentDto>>() ?? new();
                }

                _logger.LogWarning("El API devolvió un estado de error: {StatusCode}", response.StatusCode);
                return new List<AppointmentDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fallo crítico de conexión al intentar obtener citas.");
                return new List<AppointmentDto>();
            }
        }

        public async Task<bool> CreateAsync(AppointmentDto dto)
        {
            try
            {
                _logger.LogInformation("Enviando nueva cita al API para Paciente {P}", dto.PatientId);

               
                var data = new
                {
                    doctorId = dto.DoctorId,
                    patientId = dto.PatientId,
                    appointmentDate = dto.AppointmentDate,
                    notes = dto.Notes ?? "Sin observaciones",
                    status = "Pendiente"
                };

                var response = await _httpClient.PostAsJsonAsync("appointment", data);

                if (!response.IsSuccessStatusCode)
                {
                   
                    var errorBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error de negocio en el API: {Error}", errorBody);
                    return false;
                }

                _logger.LogInformation("Cita creada exitosamente a través del API.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error de comunicación con el servicio de citas.");
                return false;
            }
        }
    }
}