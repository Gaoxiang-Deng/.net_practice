
using LINQPractice.Models;

namespace LINQPractice
{
  using System;
using System.Collections.Generic;

class Program
{
    static void DisplaySortedComputerScienceStudents(List<Student> students)
    {
        var csStudents = new List<Student>();
        foreach (var student in students)
        {
            if (student.Department == "Computer Science")
            {
                csStudents.Add(student);
            }
        }

        csStudents.Sort((x, y) => y.GPA.CompareTo(x.GPA));

        Console.WriteLine("Computer Science Students (Sorted by GPA):");
        foreach (var student in csStudents)
        {
            Console.WriteLine($"{student.Name} - GPA: {student.GPA}");
        }
    }

    static void DisplayStudentCountByDepartment(List<Student> students)
    {
        var departmentCounts = new Dictionary<string, int>();

        foreach (var student in students)
        {
            if (!departmentCounts.ContainsKey(student.Department))
            {
                departmentCounts[student.Department] = 0;
            }
            departmentCounts[student.Department]++;
        }

        Console.WriteLine("\nStudent Count by Department:");
        foreach (var entry in departmentCounts)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} students");
        }
    }

    static void DisplayAverageGPA(List<Student> students)
    {
        double totalGPA = 0;
        foreach (var student in students)
        {
            totalGPA += student.GPA;
        }

        double averageGPA = students.Count > 0 ? totalGPA / students.Count : 0;
        Console.WriteLine($"\nAverage GPA: {averageGPA:F2}");
    }

    static void DisplayStudentCourses(List<Student> students, List<Course> courses)
    {
        Console.WriteLine("\nStudent Courses:");
        foreach (var student in students)
        {
            foreach (var course in courses)
            {
                if (course.StudentId == student.Id)
                {
                    Console.WriteLine($"{student.Name} - {course.CourseName}");
                }
            }
        }
    }

    static void DisplayAllStudentsWithCourses(List<Student> students, List<Course> courses)
    {
        Console.WriteLine("\nAll Students with Courses:");
        foreach (var student in students)
        {
            bool hasCourse = false;
            foreach (var course in courses)
            {
                if (course.StudentId == student.Id)
                {
                    Console.WriteLine($"{student.Name} - {course.CourseName}");
                    hasCourse = true;
                }
            }

            if (!hasCourse)
            {
                Console.WriteLine($"{student.Name} - No Course");
            }
        }
    }

    static void DisplayComputerScienceStudentsWithCourses(List<Student> students, List<Course> courses)
    {
        Console.WriteLine("\nComputer Science Students with Courses:");
        foreach (var student in students)
        {
            if (student.Department == "Computer Science")
            {
                foreach (var course in courses)
                {
                    if (course.StudentId == student.Id)
                    {
                        Console.WriteLine($"{student.Name} - {course.CourseName}");
                    }
                }
            }
        }
    }

    static void DisplayCourseCountPerStudent(List<Student> students, List<Course> courses)
    {
        Console.WriteLine("\nCourse Count per Student:");
        foreach (var student in students)
        {
            int courseCount = 0;
            foreach (var course in courses)
            {
                if (course.StudentId == student.Id)
                {
                    courseCount++;
                }
            }

            Console.WriteLine($"{student.Name} - {courseCount} courses");
        }
    }

    static void DisplayHighGPAStudentsWithCourses(List<Student> students, List<Course> courses)
    {
        Console.WriteLine("\nHigh GPA Students with Courses:");
        foreach (var student in students)
        {
            if (student.GPA > 3.5)
            {
                bool hasCourse = false;
                foreach (var course in courses)
                {
                    if (course.StudentId == student.Id)
                    {
                        Console.WriteLine($"{student.Name} - {course.CourseName}");
                        hasCourse = true;
                    }
                }

                if (!hasCourse)
                {
                    Console.WriteLine($"{student.Name} - No Course");
                }
            }
        }
    }

    static void DisplayPagedStudents(List<Student> students, int pageSize, int pageNumber)
    {
        Console.WriteLine($"\nPaged Students (Page {pageNumber}):");
        int start = (pageNumber - 1) * pageSize;
        int end = Math.Min(start + pageSize, students.Count);

        for (int i = start; i < end; i++)
        {
            Console.WriteLine($"{students[i].Name} - GPA: {students[i].GPA}");
        }
    }

    static void Main()
    {
        var students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 20, Department = "Computer Science", GPA = 3.8 },
            new Student { Id = 2, Name = "Bob", Age = 22, Department = "Mathematics", GPA = 3.5 },
            new Student { Id = 3, Name = "Charlie", Age = 21, Department = "Computer Science", GPA = 3.9 },
            new Student { Id = 4, Name = "David", Age = 23, Department = "Physics", GPA = 3.2 },
            new Student { Id = 5, Name = "Eve", Age = 20, Department = "Mathematics", GPA = 3.7 }
        };

        var courses = new List<Course>
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
        DisplayPagedStudents(students, 2, 2);
    }
}
}

