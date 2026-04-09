using MedAgenda.Application.Dtos.Patient;
using MedAgenda.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAllPatientsAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePatientDto dto)
        {
            var result = await _service.CreatePatientAsync(dto);
            return Ok(result);
        }
    }
}