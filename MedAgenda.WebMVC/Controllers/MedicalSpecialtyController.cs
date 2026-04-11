using MedAgenda.WebMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class MedicalSpecialtyController : Controller
    {
        private readonly MedicalSpecialtyService _service;

        public MedicalSpecialtyController(MedicalSpecialtyService service)
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