using MedAgenda.Application.Dtos.Specialty; // 👈 Usar la carpeta correcta
using MedAgenda.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateMedicalSpecialtyDto dto) // 👈 cambiar SaveMedicalSpecialtyDto por CreateMedicalSpecialtyDto
        {
            await _service.Add(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateMedicalSpecialtyDto dto)
        {
            await _service.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}