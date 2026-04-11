using MedAgenda.WebMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class SystemReportsController : Controller
    {
        private readonly SystemReportsService _service;

        public SystemReportsController(SystemReportsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
    }
}