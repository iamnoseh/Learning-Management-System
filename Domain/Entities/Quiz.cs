using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Quiz : BaseEntities
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public List<Question>? Questions { get; set; }
    public List<Submission>? Submissions { get; set; }
}