using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            return View(new DoctorAvailabilityDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorAvailabilityDTO dto)
        {
            if (dto.Day <= 0 || dto.ProviderId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar un doctor y un día válido.");
                return View(dto);
            }

            var success = await _service.CreateAsync(dto);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al guardar. Asegúrese de que el ID del Doctor existe en la base de datos.");
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var all = await _service.GetAllAsync();
            var item = all.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorAvailabilityDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var success = await _service.UpdateAsync(dto);
            if (success) return RedirectToAction(nameof(Index));
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