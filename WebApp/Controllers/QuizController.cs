using Domain.Dtos.QuizDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class QuizController(IQuizService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateQuiz(CreateQuizDto dto)
    {
        var res = await service.CreateQuiz(dto);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuiz(UpdateQuizDto dto)
    {
        var res = await service.UpdateQuiz(dto);
        return Ok(res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        var res = await service.DeleteQuiz(id);
        return Ok(res);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuiz(int id)
    {
        var res = await service.GetQuizById(id);
        return Ok(res);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetQuizs()
    {
        var res = await service.GetQuizs();
        return Ok(res);
    }
}