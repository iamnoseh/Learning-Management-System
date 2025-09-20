using System.Net;
using Domain.Dtos.DiscussionPostDto;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DiscussionPostService(DataContext context) : IDiscussionPostService
{
    public async Task<Response<string>> Create(CreateDiscussionDto dto)
    {
        try
        {
            var newDiscussion = new DiscussionPost
            {
                Content = dto.Content,
                UserId = dto.UserId,
                ParentId = dto.ParentId,
                CourseId = dto.CourseId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await context.DiscussionPosts.AddAsync(newDiscussion);
            var res =  await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.Created,"Discussion post created")
                : new Response<string>(HttpStatusCode.BadRequest,"Discussion post not created");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> Update(UpdateDiscussionDto dto)
    {
        try
        {
            var updaate = await context.DiscussionPosts.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (updaate == null) return new Response<string>(HttpStatusCode.NotFound,"Discussion post not found");
            updaate.Content = dto.Content;
            updaate.UserId = dto.UserId;
            updaate.ParentId = dto.ParentId;
            updaate.CourseId = dto.CourseId;
            updaate.UpdatedAt = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Discussion post updated")
                : new Response<string>(HttpStatusCode.BadRequest,"Discussion post not updated");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> Delete(int id)
    {
        try
        {
            var delete = await context.DiscussionPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (delete == null) return new Response<string>(HttpStatusCode.NotFound,"Discussion post not found");
            context.DiscussionPosts.Remove(delete);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.OK, "Discussion post deleted")
                : new Response<string>(HttpStatusCode.BadRequest, "Discussion post not deleted");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetDiscussionDto>> GetById(int id)
    {
        try
        {
            var res = await context.DiscussionPosts.FirstOrDefaultAsync(x => x.Id == id);
            if(res == null) return new Response<GetDiscussionDto>(HttpStatusCode.NotFound,"Discussion post not found");
            var dto = new GetDiscussionDto()
            {
                Content = res.Content,
                CourseId = res.CourseId,
                UserId = res.UserId,
                ParentId = res.ParentId,
                CreatedAt = res.CreatedAt,
                UpdatedAt = res.UpdatedAt
            };
            return new Response<GetDiscussionDto>(dto);
        }
        catch (Exception e)
        {
            return new Response<GetDiscussionDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetDiscussionDto>>> Get()
    {
        try
        {
            var discussion =  await context.DiscussionPosts.ToListAsync();
            if(discussion.Count == 0) return new Response<List<GetDiscussionDto>>(HttpStatusCode.NotFound,"Discussion post not found");
            var dtos = discussion.Select(x => new GetDiscussionDto()
            {
                Id = x.Id,
                Content = x.Content,
                CourseId = x.CourseId,
                UserId = x.UserId,
                ParentId = x.ParentId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return new Response<List<GetDiscussionDto>>(dtos);
        }
        catch (Exception e)
        {
            return new Response<List<GetDiscussionDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}