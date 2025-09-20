using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Course : BaseEntities
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public int CreatedBy { get; set; }
    
    public User? Creator { get; set; }
    public List<Lesson>? Lessons { get; set; }
    public List<Enrollment>? Enrollments { get; set; }
    public List<Quiz>? Quizzes { get; set; }
    public List<DiscussionPost>? DiscussionPosts { get; set; }
}