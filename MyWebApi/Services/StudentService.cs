using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Services;

public class StudentService
{
    private readonly ApplicationDbContext _context;

    public StudentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Student> GetAllStudents() => _context.Students.ToList();

    public Student? GetStudentById(int id) => _context.Students.Find(id);

    public void AddStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public bool UpdateStudent(int id, Student updatedStudent)
    {
        var student = GetStudentById(id);
        if (student == null) return false;

        student.Name = updatedStudent.Name;
        student.Major = updatedStudent.Major;
        student.GPA = updatedStudent.GPA;
        _context.SaveChanges();
        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);
        if (student == null) return false;

        _context.Students.Remove(student);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<Student> FilterByMajor(string major) =>
        _context.Students.Where(s => s.Major.Equals(major, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Student> SortByGPA(string order) =>
        order.ToLower() == "asc"
            ? _context.Students.OrderBy(s => s.GPA)
            : _context.Students.OrderByDescending(s => s.GPA);

    public IEnumerable<Student> GetStudentsByPage(int pageNumber, int pageSize) =>
        _context.Students.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
}