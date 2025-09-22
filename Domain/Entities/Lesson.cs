using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public string? FileUrl { get; set; }

    public int OrderIndex { get; set; }
    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}


