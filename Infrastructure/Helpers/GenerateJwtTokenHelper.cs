using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Helpers;

public static class GenerateJwtTokenHelper
{
    private static readonly IConfiguration configuration;
    private static readonly RoleManager<IdentityRole>  roleManager;
    private static string GenerateJwtToken(User user)
    {
        var jwtSection = configuration.GetSection("JWT");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var secret = jwtSection["Key"];
        var expiresDay = int.TryParse(jwtSection["ExpiresDay"], out var m) ? m : 3;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new ("FullName",user.FirstName+" "+user.LastName),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.PhoneNumber,user.PhoneNumber)
        };
        var roles = roleManager.Roles.ToList();
        foreach (var r in roles)
        {
            claims.Add(new Claim("role", r.Name));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(expiresDay);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
}