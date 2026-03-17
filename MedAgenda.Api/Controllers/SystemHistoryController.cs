using Microsoft.AspNetCore.Mvc;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemHistoryController : ControllerBase
    {
        private readonly ISystemHistoryRepository _repository;

        public SystemHistoryController(ISystemHistoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }
    }
}