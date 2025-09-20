namespace Domain.Dtos.AnswerDto;

public class CreateAnswerDto
{
    public string? AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
}