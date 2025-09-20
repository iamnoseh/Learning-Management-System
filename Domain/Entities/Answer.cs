using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Answer : BaseEntities
{
    public string? AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
    public Question? Question { get; set; }
    
}