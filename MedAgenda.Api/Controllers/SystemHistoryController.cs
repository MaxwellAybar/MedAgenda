using MedAgenda.Application.Dtos.SystemHistory;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemHistoryController : ControllerBase
    {
        private readonly ISystemHistoryService _service;

        public SystemHistoryController(ISystemHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllHistoriesAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetHistoryByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSystemHistoryDto dto)
        {
            var result = await _service.CreateHistoryAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSystemHistoryDto dto)
        {
            var result = await _service.UpdateHistoryAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteHistoryAsync(id);
            if (!result) return NotFound("Registro de historial no encontrado");
            return Ok();
        }
    }
}