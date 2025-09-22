using AutoMapper;
using Domain.Dtos.Courses;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(
    DataContext dataContext,
    IMapper mapper)
    : Interfaces.ICourseService
{
    public async Task<PaginationResponse<List<CourseResultDto>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var query = dataContext.Courses.AsNoTracking().OrderByDescending(c => c.CreatedAt);
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var data = mapper.Map<List<CourseResultDto>>(items);
            return new PaginationResponse<List<CourseResultDto>>(data, total, pageNumber, pageSize);
        }
        catch
        {
            return new PaginationResponse<List<CourseResultDto>>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<CourseResultDto>> GetByIdAsync(int id)
    {
        try
        {
            var course = await dataContext.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return new Response<CourseResultDto>(System.Net.HttpStatusCode.NotFound, "Course not found");
            return new Response<CourseResultDto>(mapper.Map<CourseResultDto>(course));
        }
        catch
        {
            return new Response<CourseResultDto>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<CourseResultDto>> CreateAsync(CourseCreateDto dto, int currentUserId)
    {
        try
        {
            var entity = mapper.Map<Course>(dto);
            entity.CreatedById = currentUserId;
            entity.CreatedAt = DateTime.UtcNow;
            dataContext.Courses.Add(entity);
            await dataContext.SaveChangesAsync();
            var created = await dataContext.Courses.AsNoTracking().FirstAsync(c => c.Id == entity.Id);
            return new Response<CourseResultDto>(mapper.Map<CourseResultDto>(created));
        }
        catch
        {
            return new Response<CourseResultDto>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<CourseResultDto>> UpdateAsync(int id, CourseUpdateDto dto)
    {
        try
        {
            var course = await dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return new Response<CourseResultDto>(System.Net.HttpStatusCode.NotFound, "Course not found");
            mapper.Map(dto, course);
            await dataContext.SaveChangesAsync();
            var updated = await dataContext.Courses.AsNoTracking().FirstAsync(c => c.Id == id);
            return new Response<CourseResultDto>(mapper.Map<CourseResultDto>(updated));
        }
        catch
        {
            return new Response<CourseResultDto>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var course = await dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return new Response<bool>(System.Net.HttpStatusCode.NotFound, "Course not found");
            dataContext.Courses.Remove(course);
            await dataContext.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }
}


