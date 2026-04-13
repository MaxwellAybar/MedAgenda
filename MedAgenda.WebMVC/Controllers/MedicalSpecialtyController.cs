using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalSpecialtyDto dto)
        {
            if (ModelState.IsValid)
            {
                var success = await _service.CreateAsync(dto);
                if (success) return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetAllAsync();
            var specialty = data.FirstOrDefault(x => x.Id == id);
            if (specialty == null) return NotFound();
            return View(specialty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalSpecialtyDto dto)
        {
            if (ModelState.IsValid)
            {
                var success = await _service.UpdateAsync(dto);
                if (success) return RedirectToAction(nameof(Index));
            }
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