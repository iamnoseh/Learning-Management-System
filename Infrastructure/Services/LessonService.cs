using System.Net;
using Domain.Dtos.LessonDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responces;
using Infrastructure.Data.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Services;

public class LessonService(DataContext context) : ILessonService
{
    public async Task<Response<string>> CreateLesson(CreateLessonDto dto)
    {
        try
        {
            Log.Information("Creating lesson");
            var newLesson = new Lesson()
            {
                Title = dto.Title,
                Content = dto.Content,
                VideoUrl = dto.VideoUrl,
                FileUrl = dto.FileUrl,
                OrderIndex = dto.OrderIndex,
                CourseId = dto.CourseId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            await context.Lessons.AddAsync(newLesson);
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Created lesson");
            }
            else
            {
                Log.Fatal("Answer could not be created");
            }
            return res > 0
                ? new Response<string>(HttpStatusCode.Created,"Lesson created")
                : new Response<string>(HttpStatusCode.BadRequest,"Lesson not created");
        }
        catch (Exception e)
        {
            Log.Error("Error in CreateLesson");
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateLesson(UpdateLessonDto dto)
    {
        try
        {
            Log.Information("Updating lesson");
            var updateLesson = await context.Lessons.FirstOrDefaultAsync(x=> x.Id == dto.Id);
            if(updateLesson == null) return new Response<string>(HttpStatusCode.NotFound,"Lesson not found");
            updateLesson.Title = dto.Title;
            updateLesson.Content = dto.Content;
            updateLesson.VideoUrl = dto.VideoUrl;
            updateLesson.FileUrl = dto.FileUrl;
            updateLesson.OrderIndex = dto.OrderIndex;
            updateLesson.UpdatedAt = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Updated lesson");
            }
            else
            {
                Log.Fatal("Answer could not be updated");
            }
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Lesson updated")
                : new Response<string>(HttpStatusCode.BadRequest,"Lesson not updated");
        }
        catch (Exception e)
        {
            Log.Error("Error in UpdateLesson");
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> DeleteLesson(int lessonId)
    {
        try
        {
            Log.Information("Deleting lesson");
            var deleteLesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == lessonId);
            if(deleteLesson == null) return new Response<string>(HttpStatusCode.NotFound,"Lesson not found");
            deleteLesson.IsDeleted = true;
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                Log.Information("Deleted lesson");
            }
            else
            {
                Log.Fatal("Answer could not be deleted");
            }
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Lesson deleted")
                : new Response<string>(HttpStatusCode.BadRequest,"Lesson not deleted");
        }
        catch (Exception e)
        {
            Log.Error("Error in DeleteLesson");
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetLessonDto>> GetLessonById(int lessonId)
    {
        try
        {
            Log.Information("Getting lesson");
            var getLesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == lessonId && x.IsDeleted == false);
            if(getLesson == null) return new Response<GetLessonDto>(HttpStatusCode.NotFound,"Lesson not found");
            var dto = new GetLessonDto()
            {
                Id = getLesson.Id,
                Title = getLesson.Title,
                Content = getLesson.Content,
                VideoUrl = getLesson.VideoUrl,
                FileUrl = getLesson.FileUrl,
                OrderIndex = getLesson.OrderIndex,
                CourseId = getLesson.CourseId,
                CreatedAt = getLesson.CreatedAt,
                UpdatedAt = getLesson.UpdatedAt
            };
            return new Response<GetLessonDto>(dto);
        }
        catch (Exception e)
        {
            Log.Error("Error in GetLessonById");
            return new Response<GetLessonDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PaginationResponse<List<GetLessonDto>>> GetAllLessons(LessonFilter filter)
    {
        try
        {
            Log.Information("Getting lessons");
            var query =  context.Lessons.AsQueryable();
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id);
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(x => x.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrEmpty(filter.Content))
            {
                query = query.Where(x => x.Content.Contains(filter.Content));
            }

            if (filter.OrderIndex.HasValue)
            {
                query = query.Where(x => x.OrderIndex == filter.OrderIndex);
            }

            if (filter.CourseId.HasValue)
            {
                query = query.Where(x => x.CourseId == filter.CourseId);
            }
            query =  query.Where(x=> x.IsDeleted == false);
            var total = await query.CountAsync();
            var skip = (filter.PageNumber - 1) * filter.PageSize;
            var lessons =  await query.Skip(skip).Take(filter.PageSize).ToListAsync();
            if (lessons.Count == 0)
                return new PaginationResponse<List<GetLessonDto>>(HttpStatusCode.NotFound, "Lesson not found");
            var dtos = lessons.Select(x => new GetLessonDto()
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                VideoUrl = x.VideoUrl,
                FileUrl = x.FileUrl,
                OrderIndex = x.OrderIndex,
                CourseId = x.CourseId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return new PaginationResponse<List<GetLessonDto>>(dtos, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            Log.Error("Error in GetAllLessons");
            return new PaginationResponse<List<GetLessonDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}