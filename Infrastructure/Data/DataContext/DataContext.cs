using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataContext;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
}