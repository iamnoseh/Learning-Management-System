namespace Domain.Dtos.DiscussionPostDto;

public class UpdateDiscussionDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int? ParentId { get; set; }
}