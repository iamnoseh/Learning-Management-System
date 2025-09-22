namespace Domain.Entities;

public class Submission
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    public int StudentId { get; set; }
    public User Student { get; set; } = null!;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public int Score { get; set; }
}


