using Domain.Dtos.Courses;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<PaginationResponse<List<CourseResultDto>>> GetAllAsync(int pageNumber, int pageSize);
    Task<Response<CourseResultDto>> GetByIdAsync(int id);
    Task<Response<CourseResultDto>> CreateAsync(CourseCreateDto dto, int currentUserId);
    Task<Response<CourseResultDto>> UpdateAsync(int id, CourseUpdateDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}


