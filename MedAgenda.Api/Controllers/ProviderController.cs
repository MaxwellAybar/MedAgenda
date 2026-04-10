using MedAgenda.Application.Dtos.Provider;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;

        public ProviderController(IProviderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllProvidersAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetProviderByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProviderDto dto)
        {
            await _service.CreateProviderAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProviderDto dto)
        {
            await _service.UpdateProviderAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProviderAsync(id);
            if (!result) return NotFound("Proveedor no encontrado");
            return Ok();
        }
    }
}