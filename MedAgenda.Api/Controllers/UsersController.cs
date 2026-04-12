using Microsoft.AspNetCore.Mvc;
using MedAgenda.Application.Interfaces; 
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
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
        public async Task<IActionResult> Post(Users entity)
        {
            if (entity == null) return BadRequest("Datos de usuario inválidos");

            await _repository.AddAsync(entity);
            return Ok();
        }
    }
}