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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DoctorAvailabilityDTO dto)
    {
        await _service.CreateAsync(dto);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DoctorAvailabilityDTO dto)
    {
        await _service.UpdateAsync(dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}