namespace Domain.Dtos.LessonDto;

public class GetLessonDto : UpdateLessonDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CourseId { get; set; }
}