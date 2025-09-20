using Domain.Dtos.CourseDto;
using Domain.Enums;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService service) : Controller
{
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
    {
        var res = await service.CreateCourse(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
    {
        var res = await service.UpdateCourse(dto);
        return Ok(res);
    }
    
    [HttpDelete]
    [Authorize(Roles = "Teacher , Admin , SuperAdmin")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var res = await service.DeleteCourse(id);
        return Ok(res);
    }

    [HttpGet("{Id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourse(int id)
    {
        var res = await service.GetCourseById(id);
        return Ok(res);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourses([FromQuery] CourseFilter filter)
    {
        var res = await service.GetCourses(filter);
        return Ok(res);
    }
}