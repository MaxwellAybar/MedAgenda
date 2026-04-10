using MedAgenda.Application.Dtos.SystemReports;
using MedAgenda.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemReportsController : ControllerBase
    {
        private readonly ISystemReportsService _service;

        public SystemReportsController(ISystemReportsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllReportsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetReportByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSystemReportsDto dto)
            => Ok(await _service.CreateReportAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSystemReportsDto dto)
            => Ok(await _service.UpdateReportAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteReportAsync(id);
            return result ? Ok() : NotFound("Reporte no encontrado");
        }
    }
}