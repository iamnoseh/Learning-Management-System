namespace Domain.Entities;

public class Submission : BaseEntities
{
    public DateTime SubmittedAt { get; set; }
    public double Score { get; set; }
    public int QuizId { get; set; }
    public int StudentId { get; set; }
    public Quiz? Quiz { get; set; }
    public User? Student { get; set; }
}