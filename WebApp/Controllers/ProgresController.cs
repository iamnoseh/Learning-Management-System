using Domain.Dtos.ProgresDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProgresController(IProgresService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateProgres(CreateProgresDto dto)
    {
        var result = await service.CreateProgres(dto);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProgres(UpdateProgresDto dto)
    {
        var result = await service.UpdateProgres(dto);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProgres(int id)
    {
        var result = await service.DeleteProgres(id);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProgress(int id)
    {
        var result = await service.GetProgresById(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetProgress()
    {
        var result = await service.GetAllProgress();
        return Ok(result);
    }
}