using MedAgenda.WebMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class SystemHistoryController : Controller
    {
        private readonly SystemHistoryService _service;
        private readonly ILogger<SystemHistoryController> _logger;

        public SystemHistoryController(SystemHistoryService service, ILogger<SystemHistoryController> logger)
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
                _logger.LogError(ex, "Error al cargar historial");
                return View(new List<MedAgenda.WebMVC.Models.SystemHistoryDto>());
            }
        }
    }
}