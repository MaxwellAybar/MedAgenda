using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _service;

        public AppointmentController(AppointmentService service)
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
        public async Task<IActionResult> Create(AppointmentDto dto)
        {
            dto.Status = "Pendiente";
            ModelState.Remove("Status");

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
            var appointment = data.FirstOrDefault(x => x.Id == id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentDto dto)
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