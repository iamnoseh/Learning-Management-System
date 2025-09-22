using System.Security.Claims;
using Domain.Dtos.Courses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var res = await courseService.GetAllAsync(pageNumber, pageSize);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await courseService.GetByIdAsync(id);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin,SuperAdmin,Teacher")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("UserId")?.Value;
        if (!int.TryParse(userIdClaim, out var userId)) return Unauthorized();
        var res = await courseService.CreateAsync(dto, userId);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin,SuperAdmin,Teacher")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CourseUpdateDto dto)
    {
        var res = await courseService.UpdateAsync(id, dto);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin,SuperAdmin,Teacher")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await courseService.DeleteAsync(id);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }
}


