using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=school;User=root;Password=hyP3x!sd;",
                new MySqlServerVersion(new Version(8, 0, 33))); // 替换为你的 MySQL 版本
        }
    }
}