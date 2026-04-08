using Microsoft.AspNetCore.Mvc;
using MedAgenda.Persistence.Interfaces;
using MedAgenda.Domain.Entities;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityRepository _repository;

        public DoctorAvailabilityController(IDoctorAvailabilityRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DoctorAvailability entity)
        {
            //  VALIDACIÓN ACTUALIZADA
            if (entity.StartTime >= entity.EndTime)
            {
                return BadRequest("La hora de inicio debe ser menor que la de fin");
            }

            await _repository.AddAsync(entity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(DoctorAvailability entity)
        {
            // 🔥 VALIDACIÓN
            if (entity.StartTime >= entity.EndTime)
            {
                return BadRequest("La hora de inicio debe ser menor que la de fin");
            }

            await _repository.UpdateAsync(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }

            return Ok();
        }
    }
}
