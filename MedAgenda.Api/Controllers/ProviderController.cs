using Microsoft.AspNetCore.Mvc;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderRepository _repository;

        public ProviderController(IProviderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Provider entity)
        {
            await _repository.AddAsync(entity);
            return Ok();
        }
    }
}