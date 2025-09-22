using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Courses;

public class CourseCreateDto
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? Description { get; set; }
    [StringLength(100)]
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
}

public class CourseUpdateDto
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? Description { get; set; }
    [StringLength(100)]
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
}

public class CourseResultDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public int CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
}


