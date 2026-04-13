using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        private readonly DoctorAvailabilityService _service;
        private readonly ILogger<DoctorAvailabilityController> _logger;

        public DoctorAvailabilityController(DoctorAvailabilityService service, ILogger<DoctorAvailabilityController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorAvailabilityDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            if (DateTime.TryParse(dto.StartTime, out var start) && DateTime.TryParse(dto.EndTime, out var end))
            {
                if (start >= end)
                {
                    ModelState.AddModelError(string.Empty, "La hora de inicio debe ser menor a la de fin.");
                    return View(dto);
                }
            }

            var success = await _service.CreateAsync(dto);
            if (success) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al guardar. Verifique el ID del Doctor.");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}