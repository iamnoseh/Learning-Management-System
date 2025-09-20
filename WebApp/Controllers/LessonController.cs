using Domain.Dtos.LessonDto;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LessonController(ILessonService service) : Controller
{
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateLesson(CreateLessonDto dto)
    {
        var res = await service.CreateLesson(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> UpdateLesson(UpdateLessonDto dto)
    {
        var res = await service.UpdateLesson(dto);
        return Ok(res);
    }

    [HttpDelete]
    [Authorize(Roles = "Teacher, SuperAdmin, Admin")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var res = await service.DeleteLesson(id);
        return Ok(res);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLessonById(int id)
    {
        var res = await service.GetLessonById(id);
        return Ok(res);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetLessons([FromQuery] LessonFilter filter)
    {
        var res = await service.GetAllLessons(filter);
        return Ok(res);
    }

}