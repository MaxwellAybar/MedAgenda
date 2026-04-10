using MedAgenda.Application.Dtos.Notification;
using MedAgenda.Application.Interfaces;
using MedAgenda.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllNotificationsAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetNotificationByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateNotificationDto dto)
        {
            await _service.CreateNotificationAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateNotificationDto dto)
        {
            await _service.UpdateNotificationAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteNotificationAsync(id);
            if (!result) return NotFound("Notificación no encontrada");
            return Ok();
        }
    }
}