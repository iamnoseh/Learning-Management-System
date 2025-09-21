using Domain.Dtos.QuestionDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class QuestionController(IQuestionService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateQuestion(CreateQuestionDto dto)
    {
        var res =  await service.CreateQuestion(dto);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuestion(UpdateQuestionDto dto)
    {
        var res = await service.UpdateQuestion(dto);
        return Ok(res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var res = await service.DeleteQuestion(id);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion(int id)
    {
        var res = await service.GetQuestionById(id);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions()
    {
        var res = await service.GetQuestions();
        return Ok(res);
    }
}