using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationService _service;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(NotificationService service, ILogger<NotificationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar notificaciones");
                return View(new List<NotificationDto>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NotificationDto dto)
        {
            var success = await _service.CreateAsync(dto);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar notificación");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}