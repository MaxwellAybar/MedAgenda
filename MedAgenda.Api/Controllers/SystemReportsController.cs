using MedAgenda.Application.Dtos.SystemReports;
using MedAgenda.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        {
            try
            {
                var data = await _service.GetAllReportsAsync();
                return Ok(data);
            }
            catch (Exception)
            {
                return Ok(new List<SystemReportsDto>()); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetReportByIdAsync(id);
                return result != null ? Ok(result) : NotFound();
            }
            catch (Exception)
            {
                return NotFound("Reporte no encontrado");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSystemReportsDto dto)
        {
            try
            {
                var result = await _service.CreateReportAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSystemReportsDto dto)
        {
            try
            {
                var result = await _service.UpdateReportAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteReportAsync(id);
                return result ? Ok() : NotFound("Reporte no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}