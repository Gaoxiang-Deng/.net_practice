using MyWebApi.Models;

namespace MyWebApi.Services;

public class StudentService
{
    private readonly List<Student> _students;

    public StudentService()
    {
        _students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 20 },
            new Student { Id = 2, Name = "Bob", Age = 22 },
            new Student { Id = 3, Name = "Charlie", Age = 19 }
        };
    }

    public IEnumerable<Student> GetStudents(int? page, int? size)
    {
        if (page.HasValue && size.HasValue)
        {
            return _students.Skip((page.Value - 1) * size.Value).Take(size.Value);
        }
        return _students;
    }

    public Student? GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void AddStudent(Student student)
    {
        student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
        _students.Add(student);
    }

    public bool UpdateStudent(int id, Student updatedStudent)
    {
        var student = GetStudentById(id);
        if (student == null) return false;

        student.Name = updatedStudent.Name;
        student.Age = updatedStudent.Age;
        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);
        if (student == null) return false;

        _students.Remove(student);
        return true;
    }
}