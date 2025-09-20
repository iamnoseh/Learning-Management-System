using System.Runtime.InteropServices.JavaScript;

namespace Domain.Dtos.AnswerDto;

public class GetAnswerDto : UpdateAnswerDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int QuestionId { get; set; }
}