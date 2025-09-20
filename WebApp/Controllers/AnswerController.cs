using Domain.Dtos.AnswerDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AnswerController(IAnswerService service) : Controller
{
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateAnswer(CreateAnswerDto dto)
    {
        var res =  await service.CreateAnswer(dto);
        return Ok(res);
    }

    [HttpPut]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> UpdateAnswer(UpdateAnswerDto dto)
    {
        var res = await service.UpdateAnswer(dto);
        return Ok(res);
    }

    [HttpDelete]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DeleteAnswer(int id)
    {
        var res = await service.DeleteAnswer(id);
        return Ok(res);
    }

    [HttpGet("{Id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetAnswer(int id)
    {
        var res = await service.GetAnswerById(id);
        return Ok(res);
    }

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetAnswers()
    {
        var res = await service.GetAnswers();
        return Ok(res);
    }
}