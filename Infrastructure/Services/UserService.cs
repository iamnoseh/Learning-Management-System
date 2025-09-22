using AutoMapper;
using Domain.Dtos.Users;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(
    UserManager<User> userManager,
    IEmailService emailService,
    IMapper mapper)
    : IUserService
{
    public async Task<PaginationResponse<List<UserResultDto>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var query = userManager.Users.AsNoTracking().OrderByDescending(u => u.CreatedAt);
            var total = await query.CountAsync();
            var users = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var data = mapper.Map<List<UserResultDto>>(users);
            return new PaginationResponse<List<UserResultDto>>(data, total, pageNumber, pageSize);
        }
        catch
        {
            return new PaginationResponse<List<UserResultDto>>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<UserResultDto>> GetByIdAsync(int id)
    {
        try
        {
            var user = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) 
                return new Response<UserResultDto>(System.Net.HttpStatusCode.NotFound, "User not found");
            return new Response<UserResultDto>(mapper.Map<UserResultDto>(user));
        }
        catch
        {
            return new Response<UserResultDto>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<UserResultDto>> CreateAsync(UserCreateDto dto)
    {
        try
        {
            var existsByName = await userManager.FindByNameAsync(dto.UserName);
            if (existsByName != null) return new Response<UserResultDto>(System.Net.HttpStatusCode.BadRequest, "Username already exists");

            var existsByEmail = await userManager.FindByEmailAsync(dto.Email);
            if (existsByEmail != null) return new Response<UserResultDto>(System.Net.HttpStatusCode.BadRequest, "Email already exists");

            var user = mapper.Map<User>(dto);
            var password = GenerateRandomPasswordHelper.GeneratePassword(); 
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return new Response<UserResultDto>(System.Net.HttpStatusCode.BadRequest, string.Join("; ", result.Errors.Select(e => e.Description)));
            await emailService.SendAsync(new Domain.Dtos.Email.SendEmailDto
            {
                To = user.Email!,
                Subject = "Welcome to LMS - Your Account Details",
                Body =
                    $"<p>Салом, {user.FirstName} + {user.LastName}!</p><b>Username:</b> {user.UserName}<br/><b>Password:</b> {password}</p>"
            });

            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                await userManager.AddToRoleAsync(user, dto.Role!);
            }

            var created = await userManager.Users.AsNoTracking().FirstAsync(x => x.Id == user.Id);
            return new Response<UserResultDto>(mapper.Map<UserResultDto>(created));
        }
        catch
        {
            return new Response<UserResultDto>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<UserResultDto>> UpdateAsync(int id, UserUpdateDto dto)
    {
        try
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return new Response<UserResultDto>(System.Net.HttpStatusCode.NotFound, "User not found");

            mapper.Map(dto, user);
            user.UpdatedAt = DateTime.UtcNow;
            var res = await userManager.UpdateAsync(user);
            if (!res.Succeeded) return new Response<UserResultDto>(System.Net.HttpStatusCode.BadRequest, string.Join("; ", res.Errors.Select(e => e.Description)));

            var updated = await userManager.Users.AsNoTracking().FirstAsync(x => x.Id == id);
            return new Response<UserResultDto>(mapper.Map<UserResultDto>(updated));
        }
        catch
        {
            return new Response<UserResultDto>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return new Response<bool>(System.Net.HttpStatusCode.NotFound, "User not found");
            var res = await userManager.DeleteAsync(user);
            if (!res.Succeeded) return new Response<bool>(System.Net.HttpStatusCode.BadRequest, string.Join("; ", res.Errors.Select(e => e.Description)));
            return new Response<bool>(true);
        }
        catch
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }
}


