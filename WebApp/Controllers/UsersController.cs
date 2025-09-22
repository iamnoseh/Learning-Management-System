using Domain.Dtos.Users;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var res = await userService.GetAllAsync(pageNumber, pageSize);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await userService.GetByIdAsync(id);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
    {
        var res = await userService.CreateAsync(dto);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
    {
        var res = await userService.UpdateAsync(id, dto);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await userService.DeleteAsync(id);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }
}


