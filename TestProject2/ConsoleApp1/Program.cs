using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
    }

    class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
    }

    static void Main(string[] args)
    {
        // Sample data for students
        var students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", CourseId = 101 },
            new Student { Id = 2, Name = "Bob", CourseId = 102 },
            new Student { Id = 3, Name = "Charlie", CourseId = 103 },
            new Student { Id = 4, Name = "David", CourseId = 101 }
        };

        // Sample data for courses
        var courses = new List<Course>
        {
            new Course { Id = 101, CourseName = "Mathematics" },
            new Course { Id = 102, CourseName = "Physics" },
            new Course { Id = 103, CourseName = "Chemistry" }
        };

        // Join students and courses based on CourseId
        var studentCourses = from student in students
            join course in courses on student.CourseId equals course.Id
            select new { student.Name, course.CourseName };

        // Output the result
        Console.WriteLine("Student Name | Course Name");
        Console.WriteLine("----------------------------");
        foreach (var studentCourse in studentCourses)
        {
            Console.WriteLine($"{studentCourse.Name} | {studentCourse.CourseName}");
        }
    }
}