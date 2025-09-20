using System.Net;
using Domain.Dtos.CourseDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responces;
using Infrastructure.Data.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context) : ICourseService
{
    public async Task<Response<string>> CreateCourse(CreateCourseDto dto)
    {
        try
        {
            var newCourse = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Price = dto.Price,
                IsFree = dto.IsFree,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
           await context.Courses.AddAsync(newCourse);
           var res = await context.SaveChangesAsync();
           return res > 0 
               ? new Response<string>(HttpStatusCode.Created,"Course created")
               : new Response<string>(HttpStatusCode.BadRequest,"Course not created");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto dto)
    {
        try
        {
            var updatedCourse = await context.Courses.FirstOrDefaultAsync(x=>x.Id == dto.Id);
            if(updatedCourse == null) return new Response<string>(HttpStatusCode.NotFound,"Course not found");
            updatedCourse.Title = dto.Title;
            updatedCourse.Description = dto.Description;
            updatedCourse.Category = dto.Category;
            updatedCourse.Price = dto.Price;
            updatedCourse.IsFree = dto.IsFree;
            updatedCourse.UpdatedAt = DateTime.UtcNow;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Course updated")
                : new Response<string>(HttpStatusCode.NotFound,"Course not updated");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        try
        {
            var deletedCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if(deletedCourse == null) return new Response<string>(HttpStatusCode.NotFound,"Course not found");
            deletedCourse.IsDeleted = true;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Course deleted")
                : new Response<string>(HttpStatusCode.NotFound,"Course not deleted");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        try
        {
             var getCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
             if(getCourse == null) return new Response<GetCourseDto>(HttpStatusCode.NotFound,"Course not found");
             var dto = new GetCourseDto()
             {
                 Id = getCourse.Id,
                 Title = getCourse.Title,
                 Description = getCourse.Description,
                 Category = getCourse.Category,
                 Price = getCourse.Price,
                 IsFree = getCourse.IsFree,
                 CreatedBy = getCourse.CreatedBy,
                 CreatedAt = getCourse.CreatedAt,
                 UpdatedAt = getCourse.UpdatedAt,
             };
             return new Response<GetCourseDto>(dto);
        }
        catch (Exception e)
        {
            return new Response<GetCourseDto>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<PaginationResponse<List<GetCourseDto>>> GetCourses(CourseFilter filter)
    {
        try
        {
            var query = context.Courses.AsQueryable();
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(x => x.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(x => x.Description.Contains(filter.Description));
            }

            if (filter.Category.HasValue)
            {
                query = query.Where(x => x.Category == filter.Category.Value);
            }

            if (filter.Price.HasValue)
            {
                query = query.Where(x => x.Price == filter.Price.Value);
            }

            if (filter.IsFree.HasValue)
            {
                query = query.Where(x => x.IsFree == filter.IsFree.Value);
            }

            if (filter.CreatedBy.HasValue)
            {
                query = query.Where(x => x.CreatedBy == filter.CreatedBy.Value);
            }
            query = query.Where(x=> x.IsDeleted == false);
            var total =  await query.CountAsync();
            var skip = (filter.PageNumber - 1) * filter.PageSize;
            var courses = await query.Skip(skip).Take(filter.PageSize).ToListAsync();
            if(courses.Count == 0) return new PaginationResponse<List<GetCourseDto>>(HttpStatusCode.NotFound,"No courses");
            var dtos = courses.Select(x=> new GetCourseDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Category = x.Category,
                Price = x.Price,
                IsFree = x.IsFree,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return new PaginationResponse<List<GetCourseDto>>(dtos, total, filter.PageNumber, filter.PageSize);
            
        }
        catch (Exception e)
        {
            return new PaginationResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
}