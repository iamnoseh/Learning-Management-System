using Domain.Dtos.DiscussionPostDto;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IDiscussionPostService
{
    public Task<Response<string>> Create(CreateDiscussionDto dto);
    public Task<Response<string>> Update(UpdateDiscussionDto dto);
    public Task<Response<string>> Delete(int id);
    public Task<Response<GetDiscussionDto>> GetById(int id);
    public Task<Response<List<GetDiscussionDto>>> Get();
}