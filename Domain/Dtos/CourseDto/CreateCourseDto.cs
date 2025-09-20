using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.CourseDto;

public class CreateCourseDto
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public int CreatedBy { get; set; }
}