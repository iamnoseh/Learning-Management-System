using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataContext;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<DiscussionPost> DiscussionPosts { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Progres>  Progresses { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}