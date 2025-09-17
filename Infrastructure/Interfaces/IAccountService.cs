using Domain.Dtos.Account;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IAccountService
{
    Task<Response<string>> Login(LoginDto login);
    Task<Response<string>> Register(RegisterDto register);
    Task<Response<string>> ChangePassword(ChangePasswordDto changePassword);
}