using System.Net;
using Domain.Dtos.Account;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class AccountService(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    IEmailService emailService) : IAccountService
{
    public async Task<Response<string>> Login(LoginDto login)
    {
        try
        {
            var user = await userManager.FindByNameAsync(login.Username);
            if (user == null)
                return new Response<string>(HttpStatusCode.BadRequest, "Your username or  password is incorrect");
            var res = await userManager.CheckPasswordAsync(user, login.Password);
            if (!res)
                return new Response<string>(HttpStatusCode.Unauthorized, "Your username or  password is incorrect");
            var token = await GenerateJwtTokenHelper.GenerateJwtToken(user,userManager,configuration);
            return new Response<string>(token);
        }
        catch
        {
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> Register(RegisterDto register)
    {
        try
        {
            var exists = await userManager.FindByNameAsync(register.UserName);
            if (exists != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Username already exists");

            var existingEmail = await userManager.FindByEmailAsync(register.Email);
            if (existingEmail != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Email already exists");

            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName,
                Email = register.Email,
                Gender = register.Gender,
                Address = register.Address,
                PhoneNumber = register.PhoneNumber,
                Birthday = DateOnly.FromDateTime(register.Birthday)
            };
            var password = GenerateRandomPasswordHelper.GeneratePassword();
            var result = await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user,"Student");
            if (!result.Succeeded)
                return new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");

            await emailService.SendAsync(new Domain.Dtos.Email.SendEmailDto
            {
                To = user.Email!,
                Subject = "Welcome to LMS - Your Account Details",
                Body =
                    $"<p>Салом, {user.FirstName} + {user.LastName}!</p><b>Username:</b> {user.UserName}<br/><b>Password:</b> {password}</p>"
            });

            return new Response<string>("User created and email sent");
        }
        catch (Exception)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> ChangePassword(ChangePasswordDto changePassword)
    {
        var userClaims = httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                         ?? httpContextAccessor.HttpContext?.User.FindFirst("NameId")?.Value;
        var userId = int.TryParse(userClaims, out var id);

        var user = userManager.Users.FirstOrDefault(x => x.Id == id);
        if (user == null) return new Response<string>(HttpStatusCode.BadRequest, "Something went wrong");
        var res = await userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.Password);
        if (res.Succeeded) return new Response<string>(HttpStatusCode.OK, "Your password has been changed");
        return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
    }
}