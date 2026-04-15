using Microsoft.AspNetCore.Mvc;
using MedAgenda.Application.Dtos.DoctorAvailability;
using MedAgenda.Application.Interfaces;

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
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDoctorAvailabilityDto dto)
        {
            var result = await _service.CreateAvailabilityAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateDoctorAvailabilityDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var result = await _service.UpdateAvailabilityAsync(dto);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAvailabilityAsync(id);
            return Ok(new { message = "Disponibilidad eliminada con éxito" });
        }
    }
}