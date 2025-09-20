using Domain.Enums;

namespace Domain.Entities;

public class Question : BaseEntities
{
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public int QuizId { get; set; }
    public Quiz? Quiz { get; set; }
    public List<Answer>? Answers { get; set; }
}