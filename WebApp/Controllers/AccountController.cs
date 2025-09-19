using Domain.Dtos.Account;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var res = await accountService.Login(dto);
        if (res.StatusCode == 200 ) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var res = await accountService.Register(dto);
        if (res.StatusCode == 200) return Ok(res);
        return StatusCode(res.StatusCode, res);
    }
}



