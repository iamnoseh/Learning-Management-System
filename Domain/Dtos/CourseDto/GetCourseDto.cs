namespace Domain.Dtos.CourseDto;

public class GetCourseDto : UpdateCourseDto
{
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}