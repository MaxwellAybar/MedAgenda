using Microsoft.AspNetCore.Mvc;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Dtos.Specialty;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalSpecialtyController : ControllerBase
    {
        private readonly IMedicalSpecialtyService _service;

        public MedicalSpecialtyController(IMedicalSpecialtyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var specialty = await _service.GetById(id);
                return Ok(specialty);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicalSpecialtyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Add(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMedicalSpecialtyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.Update(dto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}