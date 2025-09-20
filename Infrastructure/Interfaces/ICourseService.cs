using Domain.Dtos.CourseDto;
using Domain.Filter;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<Response<string>> CreateCourse(CreateCourseDto dto);
    Task<Response<string>> UpdateCourse(UpdateCourseDto dto);
    Task<Response<string>> DeleteCourse(int id);
    Task<Response<GetCourseDto>> GetCourseById(int id);
    Task<PaginationResponse<List<GetCourseDto>>> GetCourses(CourseFilter filter);
}