using Microsoft.AspNetCore.Mvc;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Dtos.Patient;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _service.GetAllPatientsAsync();
            return Ok(patients);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var patient = await _service.GetPatientByIdAsync(id);
                return Ok(patient);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreatePatientAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _service.UpdatePatientAsync(dto);
                return Ok(updated);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeletePatientAsync(id);

            if (!deleted)
                return NotFound(new { message = "Paciente no encontrado" });

            return NoContent();
        }
    }
}