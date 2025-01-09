using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var context = new SchoolContext();
            await context.Database.EnsureCreatedAsync();

            // 添加测试数据
            if (!context.Students.Any())
            {
                await SeedData(context);
            }

            // 查询所有学生
            await QueryStudentsAsync(context);

            // 查询所有课程及其教师
            await QueryCoursesAsync(context);

            // 查询所有学生的平均成绩
            await QueryStudentAveragesAsync(context);

            // 为一个学生添加新课程成绩
            await AddGradeAsync(context);
        }

        private static async Task QueryStudentsAsync(SchoolContext context)
        {
            var students = await context.Students
                .Select(s => new { s.Name, s.Age })
                .ToListAsync();

            Console.WriteLine("学生列表:");
            students.ForEach(s => Console.WriteLine($"姓名: {s.Name}, 年龄: {s.Age}"));
        }

        private static async Task QueryCoursesAsync(SchoolContext context)
        {
            var courses = await context.Courses
                .Select(c => new { c.Name, c.TeacherName })
                .ToListAsync();

            Console.WriteLine("\n课程列表:");
            courses.ForEach(c => Console.WriteLine($"课程: {c.Name}, 教师: {c.TeacherName}"));
        }

        private static async Task QueryStudentAveragesAsync(SchoolContext context)
        {
            var studentAverages = await context.Students
                .Select(s => new
                {
                    s.Name,
                    AverageScore = s.Grades.Any() ? s.Grades.Average(g => g.Score) : 0
                })
                .ToListAsync();

            Console.WriteLine("\n学生平均成绩:");
            studentAverages.ForEach(s => Console.WriteLine($"姓名: {s.Name}, 平均成绩: {s.AverageScore:F2}"));
        }

        private static async Task AddGradeAsync(SchoolContext context)
        {
            var student = await context.Students.FirstOrDefaultAsync();
            var course = await context.Courses.FirstOrDefaultAsync();

            if (student != null && course != null)
            {
                context.Grades.Add(new Grade
                {
                    StudentId = student.Id,
                    CourseId = course.Id,
                    Score = 92.5
                });

                await context.SaveChangesAsync();
                Console.WriteLine($"\n已为学生 {student.Name} 添加课程 {course.Name} 的成绩: 92.5");
            }
        }

        private static async Task SeedData(SchoolContext context)
        {
            var students = new[]
            {
                new Student { Name = "Alice", Age = 20 },
                new Student { Name = "Bob", Age = 22 }
            };
            var courses = new[]
            {
                new Course { Name = "Mathematics", TeacherName = "Dr. Smith" },
                new Course { Name = "Physics", TeacherName = "Dr. Brown" }
            };

            context.Students.AddRange(students);
            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
        }
    }
}
