using MedAgenda.WebMVC.Models;
using MedAgenda.WebMVC.Services;
using Microsoft.AspNetCore.Mvc;

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

    
    [HttpPost]
    public async Task<IActionResult> Create(DoctorAvailabilityDTO dto)
    {
        await _service.CreateAsync(dto);
        return RedirectToAction("Index");
    }
}