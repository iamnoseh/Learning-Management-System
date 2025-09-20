using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DiscussionPost : BaseEntities
{
    [Required] 
    public required string Content { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int? ParentId { get; set; }
    public Course? Course { get; set; }
    public User? User { get; set; }
    
}