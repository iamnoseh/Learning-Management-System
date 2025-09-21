using System.Net;
using Domain.Dtos.Enrollment;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EnrollmentService(DataContext context) : IEnrollmentService
{
    public async Task<Response<string>> CreateEnrollment(CreateEnrollmentDto dto)
    {
        try
        {
            var newEnrollment = new Enrollment()
            {
                IsPremium = dto.IsPremium,
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                EnrollmentDate = DateTime.UtcNow,
                ExpiryDate = dto.IsPremium
                    ? DateTime.UtcNow.AddMonths(6)
                    : DateTime.UtcNow.AddDays(15),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await context.Enrollments.AddAsync(newEnrollment);
            var res = await context.SaveChangesAsync();
            return res > 0 
                ? new Response<string>(HttpStatusCode.Created,"Enrollment created")
                : new Response<string>(HttpStatusCode.NotFound,"Enrollment not created");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateEnrollment(UpdateEnrollmentDto dto)
    {
        try
        {
            var updatedEnrollment = await context.Enrollments.FirstOrDefaultAsync(x=> x.Id == dto.Id);
            if (updatedEnrollment == null) return new Response<string>(HttpStatusCode.NotFound,"Enrollment not found");
            updatedEnrollment.IsPremium = dto.IsPremium;
            updatedEnrollment.ExpiryDate = dto.IsPremium
                ? DateTime.UtcNow.AddMonths(6)
                : DateTime.UtcNow.AddDays(15);
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Enrollment updated")
                : new Response<string>(HttpStatusCode.NotFound,"Enrollment not updated");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        try
        {
            var deletedEnrollment = await context.Enrollments
                .FirstOrDefaultAsync(x => x.Id == id);
            if (deletedEnrollment == null) return new Response<string>(HttpStatusCode.NotFound,"Enrollment not found"); 
            deletedEnrollment.IsDeleted = true;
            var res = await context.SaveChangesAsync();
            return res > 0
                ? new Response<string>(HttpStatusCode.OK,"Enrollment deleted")
                : new Response<string>(HttpStatusCode.NotFound,"Enrollment not deleted");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollmentBystudentId(int studentId)
    {
        try
        {
            var enrollment = await context.Enrollments
                .Include(x => x.Student)
                .Where(x => x.StudentId == studentId && x.IsDeleted == false).ToListAsync();
            if(enrollment.Count == 0) return new Response<List<GetEnrollmentDto>>(HttpStatusCode.NotFound,"Enrollment not found");
            var dto = enrollment.Select(x => new GetEnrollmentDto()
            {
                Id = x.Id,
                IsPremium = x.IsPremium,
                CourseId = x.CourseId,
                StudentId = x.StudentId,
                EnrollmentDate = x.EnrollmentDate,
                ExpiryDate = x.ExpiryDate,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return new Response<List<GetEnrollmentDto>>(dto);
        }
        catch (Exception e)
        {
            return new Response<List<GetEnrollmentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollments()
    {
        try
        {
            var enrollments = await context.Enrollments.Where(x=>x.IsDeleted==false).ToListAsync();
            if(enrollments.Count == 0) return new Response<List<GetEnrollmentDto>>(HttpStatusCode.NotFound,"Enrollments not found");
            var dtos = enrollments.Select(x=> new GetEnrollmentDto()
            {
                Id = x.Id,
                IsPremium = x.IsPremium,
                CourseId = x.CourseId,
                StudentId = x.StudentId,
                EnrollmentDate = x.EnrollmentDate,
                ExpiryDate = x.ExpiryDate,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
            return new Response<List<GetEnrollmentDto>>(dtos);
        }
        catch (Exception e)
        {
            return new Response<List<GetEnrollmentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}