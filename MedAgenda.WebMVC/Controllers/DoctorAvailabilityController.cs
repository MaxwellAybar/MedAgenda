using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class DoctorAvailabilityController : Controller
    {
        private readonly DoctorAvailabilityService _service;

        public DoctorAvailabilityController(DoctorAvailabilityService service)
        {
            _service = service;
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}