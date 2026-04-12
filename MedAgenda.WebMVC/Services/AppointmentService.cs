using MedAgenda.WebMVC.Models;
using System.Net.Http.Json;

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
                return await _httpClient.GetFromJsonAsync<List<AppointmentDto>>("appointment") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de citas.");
                return new();
            }
        }

        public async Task<bool> CreateAsync(AppointmentDto dto)
        {
            try
            {


                var data = new
                {
                    doctorId = dto.DoctorId,
                    patientId = dto.PatientId,
                    appointmentDate = dto.AppointmentDate,
                    notes = dto.Notes ?? "Cita de prueba",
                    status = "Pendiente"
                };

                var response = await _httpClient.PostAsJsonAsync("appointment", data);

                if (!response.IsSuccessStatusCode)
                {

                    var errorBody = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"--- ERROR DEL API ---: {errorBody}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"--- ERROR DE CONEXIÓN ---: {ex.Message}");
                return false;
            }
        }
    }
}
    