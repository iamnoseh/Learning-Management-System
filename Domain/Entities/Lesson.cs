using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Lesson : BaseEntities
{
    [Required]
    public required string Title { get; set; }
    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public string? FileUrl { get; set; }
    public int OrderIndex { get; set; }
    public int CourseId { get; set; }
    
    public Course? Course { get; set; }
    public List<Progres>? ProgresList { get; set; }
    
}