using Domain.Dtos.LessonDto;
using Domain.Filter;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ILessonService
{
    public Task<Response<string>> CreateLesson(CreateLessonDto dto);
    public Task<Response<string>> UpdateLesson(UpdateLessonDto dto);
    public Task<Response<string>> DeleteLesson(int lessonId);
    public Task<Response<GetLessonDto>> GetLessonById(int  lessonId);
    public Task<PaginationResponse<List<GetLessonDto>>> GetAllLessons(LessonFilter filter);
}