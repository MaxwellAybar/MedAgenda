using Microsoft.AspNetCore.Mvc;
using MedAgenda.Application.Dtos.DoctorAvailability;
using MedAgenda.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _service;

        public DoctorAvailabilityController(IDoctorAvailabilityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var data = await _service.GetAvailabilitiesByDoctorAsync(1);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetAvailabilityByIdAsync(id);
            if (data == null) return NotFound("Disponibilidad no encontrada");
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDoctorAvailabilityDto dto)
        {
           
            if (dto.StartTime >= dto.EndTime)
                return BadRequest("La hora de inicio debe ser menor que la de fin");

            var result = await _service.CreateAvailabilityAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateDoctorAvailabilityDto dto)
        {
            if (dto.StartTime >= dto.EndTime)
                return BadRequest("La hora de inicio debe ser menor que la de fin");

            var result = await _service.UpdateAvailabilityAsync(dto);
            if (result == null) return NotFound("Disponibilidad no encontrada");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAvailabilityAsync(id);
            if (!success) return NotFound("Disponibilidad no encontrada");

           
            return Ok(new { message = "Disponibilidad eliminada con éxito" });
        }
    }
}