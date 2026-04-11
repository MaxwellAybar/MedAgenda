using MedAgenda.WebMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class SystemHistoryController : Controller
    {
        private readonly SystemHistoryService _service;

        public SystemHistoryController(SystemHistoryService service)
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