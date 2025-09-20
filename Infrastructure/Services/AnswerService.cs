using System.Net;
using Domain.Dtos.AnswerDto;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Services;

public class AnswerService(DataContext context) : IAnswerService
{
    public async Task<Response<string>> CreateAnswer(CreateAnswerDto dto)
    {
        try
        {
            Log.Information("Creating a new answer");
            var newAnswer = new Answer()
            {
                AnswerText = dto.AnswerText,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await context.Answers.AddAsync(newAnswer);
            var res =  await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Answer was created");
            }
            else
            {
                Log.Fatal("Answer could not be created");
            }
            return res > 0 
                ? new Response<string>(HttpStatusCode.Created,"Answer Created")
                : new Response<string>(HttpStatusCode.BadRequest,"Answer not created");
        }
        catch (Exception e)
        {
            Log.Error("Error creating answer");
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<string>> UpdateAnswer(UpdateAnswerDto dto)
    {
        try
        {
            Log.Information("Updating a new answer");
            var updateAnswer = await context.Answers.FirstOrDefaultAsync(x=> x.Id == dto.Id);
            updateAnswer.AnswerText = dto.AnswerText;
            updateAnswer.IsCorrect = dto.IsCorrect;
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Answer updated");
            }
            else
            {
                Log.Fatal("Answer not updated");
            }
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Answer updated")
                : new Response<string>(HttpStatusCode.BadRequest,"Answer not updated");
        }
        catch (Exception e)
        {
            Log.Error("Error in UpdateAnswer");
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<string>> DeleteAnswer(int answerId)
    {
        try
        {
            Log.Information("DeleteAnswer ");
            var deleteAnswer = await context.Answers.FirstOrDefaultAsync(x => x.Id == answerId);
            context.Answers.Remove(deleteAnswer);
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Answer deleted successfully");
            }
            else
            {
                Log.Fatal("Answer not deleted");
            }
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Answer deleted")
                : new Response<string>(HttpStatusCode.BadRequest,"Answer not deleted");
        }
        catch (Exception e)
        {
            Log.Error("Error deleting answer");
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<GetAnswerDto>> GetAnswerById(int answerId)
    {
        try
        {
            Log.Information("GetAnswerById called");
            var getAnswer = await context.Answers.FirstOrDefaultAsync(x => x.Id == answerId);
            if(getAnswer == null) return new Response<GetAnswerDto>(HttpStatusCode.NotFound,"Answer not found");
            var dto = new GetAnswerDto()
            {
                Id = getAnswer.Id,
                AnswerText = getAnswer.AnswerText,
                IsCorrect = getAnswer.IsCorrect,
                QuestionId = getAnswer.QuestionId,
                CreatedAt = getAnswer.CreatedAt,
                UpdatedAt = getAnswer.UpdatedAt
            };
            return new Response<GetAnswerDto>(dto);
        }
        catch (Exception e)
        {
            Log.Error("Error getting answer by id");
            return new Response<GetAnswerDto>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<List<GetAnswerDto>>> GetAnswers()
    {
        try
        {
            Log.Information("Get Answers");
            var getAnswers = await context.Answers.ToListAsync();
            if(getAnswers.Count == 0) return new Response<List<GetAnswerDto>>(HttpStatusCode.NotFound,"Answer not found");
            var dtos = getAnswers.Select(x => new GetAnswerDto()
            {
                Id = x.Id,
                AnswerText = x.AnswerText,
                IsCorrect = x.IsCorrect,
                QuestionId = x.QuestionId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return new Response<List<GetAnswerDto>>(dtos);
        }
        catch (Exception e)
        {
            Log.Error("Error in Get Answers");
            return new Response<List<GetAnswerDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
}