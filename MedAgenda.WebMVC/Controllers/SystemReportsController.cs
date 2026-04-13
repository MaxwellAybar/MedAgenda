using MedAgenda.WebMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class SystemReportsController : Controller
    {
        private readonly SystemReportsService _service;
        private readonly ILogger<SystemReportsController> _logger;

        public SystemReportsController(SystemReportsService service, ILogger<SystemReportsController> logger)
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
                _logger.LogError(ex, "Error al cargar reportes");
                return View(new List<MedAgenda.WebMVC.Models.SystemReportsDto>());
            }
        }
    }
}