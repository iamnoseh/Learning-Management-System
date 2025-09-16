using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeder;

public static class Seed
{
    public static async Task SeedSuperAdmin(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        if (!roleManager.RoleExistsAsync(Role.SuperAdmin.ToString()).Result)
        {
           await roleManager.CreateAsync(new IdentityRole<int>("SuperAdmin"));
        }

        var user = userManager.Users.FirstOrDefault(x => x.UserName == "SuperAdmin");
        if (user == null)
        {
            var newUser = new User()
            {
                FirstName = "Super",
                LastName = "Admin",
                PhoneNumber = "987676767",
                Address = "Dushanbe",
                UserName = "SuperAdmin",
                Gender = Gender.Male,
                Email = "admin@mail.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            var res = userManager.CreateAsync(newUser, "123Qwerty!");
            if (res.Result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, Role.SuperAdmin.ToString());
            }
        }
    }
    
    public static async Task<bool> SeedRole(RoleManager<IdentityRole<int>> roleManage)
    {
        var newRoles = new List<IdentityRole<int>>()
        {
            new(Role.SuperAdmin.ToString()),
            new(Role.Admin.ToString()),
            new(Role.Teacher.ToString()),
            new(Role.Student.ToString()),
        };
        var roles = await roleManage.Roles.ToListAsync(); 
        foreach (var role in newRoles)
        {
            if (roles.Any(e => e.Name == role.Name))
                continue;
            await roleManage.CreateAsync(role);
        }
        return true;
    }
}