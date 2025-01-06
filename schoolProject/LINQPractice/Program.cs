
using LINQPractice.Models;

namespace LINQPractice
{
    using System;
    using System.Linq;
    public class Program
    {
        static void Main()
        {
            var students = new Student[]
            {
                new Student { Id = 1, Name = "Alice", Age = 20, Department = "Computer Science", GPA = 3.8 },
                new Student { Id = 2, Name = "Bob", Age = 22, Department = "Mathematics", GPA = 3.5 },
                new Student { Id = 3, Name = "Charlie", Age = 21, Department = "Computer Science", GPA = 3.9 },
                new Student { Id = 4, Name = "David", Age = 23, Department = "Physics", GPA = 3.2 },
                new Student { Id = 5, Name = "Eve", Age = 20, Department = "Mathematics", GPA = 3.7 }
            };

            var courses = new Course[]
            {
                new Course { StudentId = 1, CourseName = "Programming 101" },
                new Course { StudentId = 1, CourseName = "Algorithms" },
                new Course { StudentId = 2, CourseName = "Calculus" },
                new Course { StudentId = 3, CourseName = "Data Structures" },
                new Course { StudentId = 3, CourseName = "Operating Systems" },
                new Course { StudentId = 4, CourseName = "Quantum Mechanics" },
                new Course { StudentId = 5, CourseName = "Linear Algebra" }
            };

            DisplaySortedComputerScienceStudents(students);
            DisplayStudentCountByDepartment(students);
            DisplayAverageGPA(students);
            DisplayStudentCourses(students, courses);
            DisplayAllStudentsWithCourses(students, courses);
            DisplayComputerScienceStudentsWithCourses(students, courses);
            DisplayCourseCountPerStudent(students, courses);
            DisplayHighGPAStudentsWithCourses(students, courses);
            DisplayDepartmentStats(students, courses);
            DisplayPagedStudents(students, 2, 2);
        }

        static void DisplaySortedComputerScienceStudents(Student[] students)
        {
            var csStudents = students
                .Where(s => s.Department == "Computer Science")
                .OrderByDescending(s => s.GPA);

            Console.WriteLine("Computer Science Students (Sorted by GPA):");
            foreach (var student in csStudents)
            {
                Console.WriteLine($"{student.Name} - GPA: {student.GPA}");
            }
        }

        static void DisplayStudentCountByDepartment(Student[] students)
        {
            var departmentGroups = students
                .GroupBy(s => s.Department)
                .Select(group => new { Department = group.Key, Count = group.Count() });

            Console.WriteLine("\nStudent Count by Department:");
            foreach (var group in departmentGroups)
            {
                Console.WriteLine($"{group.Department}: {group.Count} students");
            }
        }

        static void DisplayAverageGPA(Student[] students)
        {
            var averageGPA = students.Average(s => s.GPA);
            Console.WriteLine($"\nAverage GPA: {averageGPA:F2}");
        }

        static void DisplayStudentCourses(Student[] students, Course[] courses)
        {
            var studentCourses = from s in students
                                 join c in courses on s.Id equals c.StudentId
                                 select new { s.Name, c.CourseName };

            Console.WriteLine("\nStudent Courses:");
            foreach (var sc in studentCourses)
            {
                Console.WriteLine($"{sc.Name} - {sc.CourseName}");
            }
        }

        static void DisplayAllStudentsWithCourses(Student[] students, Course[] courses)
        {
            var leftJoin = from s in students
                           join c in courses on s.Id equals c.StudentId into scGroup
                           from sc in scGroup.DefaultIfEmpty()
                           select new { s.Name, Course = sc?.CourseName ?? "No Course" };

            Console.WriteLine("\nAll Students with Courses:");
            foreach (var entry in leftJoin)
            {
                Console.WriteLine($"{entry.Name} - {entry.Course}");
            }
        }

        static void DisplayComputerScienceStudentsWithCourses(Student[] students, Course[] courses)
        {
            var csStudentCourses = from s in students
                                   join c in courses on s.Id equals c.StudentId
                                   where s.Department == "Computer Science"
                                   select new { s.Name, c.CourseName };

            Console.WriteLine("\nComputer Science Students with Courses:");
            foreach (var entry in csStudentCourses)
            {
                Console.WriteLine($"{entry.Name} - {entry.CourseName}");
            }
        }

        static void DisplayCourseCountPerStudent(Student[] students, Course[] courses)
        {
            var courseCounts = from s in students
                               join c in courses on s.Id equals c.StudentId into scGroup
                               select new { s.Name, CourseCount = scGroup.Count() };

            Console.WriteLine("\nCourse Count per Student:");
            foreach (var entry in courseCounts)
            {
                Console.WriteLine($"{entry.Name} - {entry.CourseCount} courses");
            }
        }

        static void DisplayHighGPAStudentsWithCourses(Student[] students, Course[] courses)
        {
            var highGPAStudents = from s in students
                                  where s.GPA > 3.5
                                  join c in courses on s.Id equals c.StudentId into scGroup
                                  from sc in scGroup.DefaultIfEmpty()
                                  orderby s.Name
                                  select new { s.Name, Course = sc?.CourseName ?? "No Course" };

            Console.WriteLine("\nHigh GPA Students with Courses:");
            foreach (var entry in highGPAStudents)
            {
                Console.WriteLine($"{entry.Name} - {entry.Course}");
            }
        }

        static void DisplayDepartmentStats(Student[] students, Course[] courses)
        {
            var departmentStats = from s in students
                                  group s by s.Department into deptGroup
                                  select new
                                  {
                                      Department = deptGroup.Key,
                                      AverageGPA = deptGroup.Average(s => s.GPA),
                                      Students = from s in deptGroup
                                                 join c in courses on s.Id equals c.StudentId into scGroup
                                                 select new { s.Name, Courses = scGroup.Select(c => c.CourseName).ToList() }
                                  };

            Console.WriteLine("\nDepartment Stats:");
            foreach (var dept in departmentStats)
            {
                Console.WriteLine($"Department: {dept.Department}, Average GPA: {dept.AverageGPA:F2}");
                foreach (var student in dept.Students)
                {
                    Console.WriteLine($"  {student.Name} - Courses: {string.Join(", ", student.Courses)}");
                }
            }
        }

        static void DisplayPagedStudents(Student[] students, int pageSize, int pageNumber)
        {
            var pagedStudents = students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            Console.WriteLine($"\nPaged Students (Page {pageNumber}):");
            foreach (var student in pagedStudents)
            {
                Console.WriteLine($"{student.Name} - GPA: {student.GPA}");
            }
        }
    }
}

