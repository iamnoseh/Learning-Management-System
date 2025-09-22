using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    [Required]
    public string QuestionText { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}


