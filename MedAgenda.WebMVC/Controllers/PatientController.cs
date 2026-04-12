using MedAgenda.WebMVC.Services;
using MedAgenda.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.WebMVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _service;
        private readonly ILogger<PatientController> _logger;

        
        public PatientController(PatientService service, ILogger<PatientController> logger)
        {
            _service = service;
            _logger = logger;
        }

        
        public async Task<IActionResult> Index()
        {
            var patients = await _service.GetAllAsync();
            return View(patients);
        }

    
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(PatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El modelo de paciente no es válido.");
                return View(dto);
            }

            try
            {
                var success = await _service.CreateAsync(dto);

                if (success)
                {
                    _logger.LogInformation("Paciente {FirstName} registrado con éxito", dto.FirstName);
                    return RedirectToAction(nameof(Index));
                }

           
                _logger.LogError("El API rechazó la creación del paciente.");
                ModelState.AddModelError(string.Empty, "Hubo un problema con el servidor al guardar el paciente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al intentar crear un paciente.");
                ModelState.AddModelError(string.Empty, "Error de conexión con el servicio.");
            }

            return View(dto);
        }
    }
}