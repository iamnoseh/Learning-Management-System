using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.DiscussionPostDto;

public class CreateDiscussionDto
{
    [Required] 
    public required string Content { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int? ParentId { get; set; }
}