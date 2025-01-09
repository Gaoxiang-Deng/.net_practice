using MyWebApi.Models;

namespace MyWebApi.Services;

public class StudentService
{
    private readonly List<Student> _students;

    public StudentService()
    {
        _students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Major = "Computer Science", GPA = 3.8 },
            new Student { Id = 2, Name = "Bob", Major = "Mathematics", GPA = 3.5 },
            new Student { Id = 3, Name = "Charlie", Major = "Physics", GPA = 3.9 }
        };
    }

    public IEnumerable<Student> GetAllStudents() => _students;

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
        student.Major = updatedStudent.Major;
        student.GPA = updatedStudent.GPA;
        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);
        if (student == null) return false;

        _students.Remove(student);
        return true;
    }

    public IEnumerable<Student> FilterByMajor(string major) =>
        _students.Where(s => s.Major.Equals(major, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Student> SortByGPA(string order) =>
        order.ToLower() == "asc"
            ? _students.OrderBy(s => s.GPA)
            : _students.OrderByDescending(s => s.GPA);

    public IEnumerable<Student> GetStudentsByPage(int pageNumber, int pageSize) =>
        _students.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}