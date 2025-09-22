namespace Domain.Entities;

public class DiscussionPost
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    public int? ParentId { get; set; }
    public DiscussionPost? Parent { get; set; }
    public ICollection<DiscussionPost> Replies { get; set; } = new List<DiscussionPost>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


