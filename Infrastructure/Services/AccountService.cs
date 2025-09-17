using System.Net;
using Domain.Dtos.Account;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AccountService(
    UserManager<User> userManager
    ,IHttpContextAccessor httpContextAccessor) : IAccountService
{
    public async Task<Response<string>> Login(LoginDto login)
    {
        try
        {
            var user = await userManager.FindByNameAsync(login.Username);
            if (user == null) return new  Response<string>(HttpStatusCode.BadRequest,"Your username or  password is incorrect");
            var res =  await userManager.CheckPasswordAsync(user, login.Password);
            if (res)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                return new  Response<string>(token);
            }
            return new  Response<string>(HttpStatusCode.Unauthorized,"Your username or  password is incorrect");
        }
        catch (Exception e)
        {
            return new  Response<string>(HttpStatusCode.InternalServerError,"Something went wrong");
        }
    }

    public async Task<Response<string>> Register(RegisterDto register)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> ChangePassword(ChangePasswordDto changePassword)
    {
        var userClaims =  httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                         ?? httpContextAccessor.HttpContext?.User.FindFirst("NameId")?.Value;
        var userId = int.TryParse(userClaims, out var id);

        var user = userManager.Users.FirstOrDefault(x => x.Id == id);
        if (user == null) return new  Response<string>(HttpStatusCode.BadRequest,"Something went wrong");
        var res =await userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.Password);
        if(res.Succeeded) return new Response<string>(HttpStatusCode.OK,"Your password has been changed");
        return new  Response<string>(HttpStatusCode.InternalServerError,"Something went wrong");
    }
}