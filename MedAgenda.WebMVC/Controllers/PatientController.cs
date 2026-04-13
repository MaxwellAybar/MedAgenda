using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _service;

        public PatientController(PatientService service)
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var success = await _service.CreateAsync(dto);
            if (success) return RedirectToAction(nameof(Index));
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PatientDto dto)
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