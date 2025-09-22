using Domain.Dtos.Users;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<PaginationResponse<List<UserResultDto>>> GetAllAsync(int pageNumber, int pageSize);
    Task<Response<UserResultDto>> GetByIdAsync(int id);
    Task<Response<UserResultDto>> CreateAsync(UserCreateDto dto);
    Task<Response<UserResultDto>> UpdateAsync(int id, UserUpdateDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}


