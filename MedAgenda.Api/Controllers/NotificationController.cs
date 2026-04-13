using MedAgenda.Application.Dtos.Notification;
using MedAgenda.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var data = await _service.GetAllNotificationsAsync();
                return Ok(data);
            }
            catch (Exception)
            {
                return Ok(new List<NotificationDto>()); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await _service.GetNotificationByIdAsync(id);
                return Ok(data);
            }
            catch (Exception)
            {
                return NotFound("Notificación no encontrada");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateNotificationDto dto)
        {
            try
            {
                await _service.CreateNotificationAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateNotificationDto dto)
        {
            try
            {
                await _service.UpdateNotificationAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteNotificationAsync(id);
                if (!result) return NotFound("Notificación no encontrada");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}