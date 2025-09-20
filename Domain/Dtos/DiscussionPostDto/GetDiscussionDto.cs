namespace Domain.Dtos.DiscussionPostDto;

public class GetDiscussionDto : UpdateDiscussionDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}