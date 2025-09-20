using Domain.Enums;

namespace Domain.Dtos.CourseDto;

public class UpdateCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
}