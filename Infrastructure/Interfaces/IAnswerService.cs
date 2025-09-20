using Domain.Dtos.AnswerDto;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IAnswerService
{
    public Task<Response<string>> CreateAnswer(CreateAnswerDto dto);
    public Task<Response<string>> UpdateAnswer(UpdateAnswerDto dto);
    public Task<Response<string>> DeleteAnswer(int answerId);
    public Task<Response<GetAnswerDto>> GetAnswerById(int answerId);
    public Task<Response<List<GetAnswerDto>>> GetAnswers();
}