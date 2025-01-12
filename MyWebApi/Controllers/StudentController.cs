using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("students")]
public class StudentController(StudentService service) : ControllerBase
{
    private readonly StudentService _service = service;

    [HttpGet]
    public IActionResult GetStudents([FromQuery] string? major, [FromQuery] string? order, [FromQuery] int? page, [FromQuery] int? size)
    {
        var students = _service.GetAllStudents();

        if (!string.IsNullOrEmpty(major))
        {
            students = _service.FilterByMajor(major);
        }

        if (!string.IsNullOrEmpty(order))
        {
            students = _service.SortByGPA(order);
        }

        if (page.HasValue && size.HasValue)
        {
            students = _service.GetStudentsByPage(students, page.Value, size.Value);
        }

        return Ok(students);
    }

    [HttpGet("filter")]
    public IActionResult FilterStudents([FromQuery] string? major)
    {
        if (string.IsNullOrEmpty(major))
        {
            return BadRequest(new { Message = "Major query parameter is required." });
        }

        var students = _service.FilterByMajor(major);
        if (!students.Any())
        {
            return NotFound(new { Message = $"No students found for major: {major}" });
        }

        return Ok(students);
    }

    [HttpGet("sort")]
    public IActionResult SortStudents([FromQuery] string order = "asc")
    {
        var students = _service.SortByGPA(order);
        if (!students.Any())
        {
            return NotFound(new { Message = "No students found to sort." });
        }

        return Ok(students);
    }

    [HttpGet("page")]
    public IActionResult PaginateStudents([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var students = _service.GetStudentsByPage(_service.GetAllStudents(), page, size);
        if (!students.Any())
        {
            return NotFound(new { Message = $"No students found for page {page} with size {size}." });
        }

        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _service.GetStudentById(id);
        if (student == null) return NotFound(new { Message = $"Student with ID {id} not found." });
        return Ok(student);
    }

    [HttpPost]
    public IActionResult AddStudent([FromBody] Student student)
    {
        _service.AddStudent(student);
        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        if (!_service.UpdateStudent(id, updatedStudent))
        {
            return NotFound(new { Message = $"Student with ID {id} not found." });
        }

        var student = _service.GetStudentById(id); // 获取更新后的学生信息
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        if (!_service.DeleteStudent(id))
        {
            return NotFound(new { Message = $"Student with ID {id} not found." });
        }
        return Ok(new { Message = $"Student with ID {id} deleted successfully." });
    }
}
