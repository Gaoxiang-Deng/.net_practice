using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }

    public static void SeedData(ApplicationDbContext context)
    {
        if (!context.Students.Any())
        {
            context.Students.AddRange(
                new Student { Name = "Alice", Major = "Computer Science", GPA = 3.8 },
                new Student { Name = "Bob", Major = "Mathematics", GPA = 3.5 },
                new Student { Name = "Charlie", Major = "Physics", GPA = 3.9 }
            );
            context.SaveChanges();
        }
    }
}