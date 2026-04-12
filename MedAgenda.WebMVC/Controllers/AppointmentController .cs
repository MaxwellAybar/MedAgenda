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
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

            
                ModelState.AddModelError(string.Empty, "No se pudo registrar la cita. Verifique que el ID del Doctor y el Paciente sean correctos.");
            }

            return View(dto);
        }
    }
}