using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ProviderService _service;

        public ProviderController(ProviderService service)
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
        public async Task<IActionResult> Create(ProviderDto dto)
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
            var provider = data.FirstOrDefault(x => x.Id == id);
            if (provider == null) return NotFound();
            return View(provider);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProviderDto dto)
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