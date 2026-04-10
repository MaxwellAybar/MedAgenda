using Microsoft.AspNetCore.Mvc;
using MedAgenda.Application.Dtos.Appointment;
using MedAgenda.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAppointmentsByPatientAsync(0); 
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await _service.GetAppointmentByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateAppointmentDto dto)
        {
            try
            {
                var result = await _service.RequestAppointmentAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateAppointmentDto dto)
        {
            try
            {
                var result = await _service.UpdateAppointmentAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.CancelAppointmentAsync(id);
            if (success) return Ok();
            return NotFound("Cita no encontrada o ya cancelada");
        }
    }
}