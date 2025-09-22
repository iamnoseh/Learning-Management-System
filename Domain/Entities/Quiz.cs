using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Quiz
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}


