using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

public static class StudentController
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        var route = "/students";

        // Add student
        app.MapPost(route, ([FromServices] StudentService service, [FromBody] Student student) =>
        {
            service.AddStudent(student);
            return Results.Created($"{route}/{student.Id}", student);
        }).WithName("AddStudent");

        // Get all students
        app.MapGet(route, ([FromServices] StudentService service) =>
        {
            return Results.Ok(service.GetAllStudents());
        }).WithName("GetAllStudents");

        // Get student by ID
        app.MapGet($"{route}/{{id}}", ([FromServices] StudentService service, int id) =>
        {
            var student = service.GetStudentById(id);
            if (student == null) return Results.NotFound($"Student with ID {id} not found.");
            return Results.Ok(student);
        }).WithName("GetStudentById");

        // Update student
        app.MapPut($"{route}/{{id}}", ([FromServices] StudentService service, int id, [FromBody] Student updatedStudent) =>
        {
            if (!service.UpdateStudent(id, updatedStudent))
            {
                return Results.NotFound($"Student with ID {id} not found.");
            }
            return Results.Ok(updatedStudent);
        }).WithName("UpdateStudent");

        // Delete student
        app.MapDelete($"{route}/{{id}}", ([FromServices] StudentService service, int id) =>
        {
            if (!service.DeleteStudent(id))
            {
                return Results.NotFound($"Student with ID {id} not found.");
            }
            return Results.Ok($"Student with ID {id} deleted successfully.");
        }).WithName("DeleteStudent");

        // Filter by major
        app.MapGet($"{route}/filter", ([FromServices] StudentService service, [FromQuery] string major) =>
        {
            var students = service.FilterByMajor(major);
            if (!students.Any()) return Results.NotFound($"No students found for major {major}.");
            return Results.Ok(students);
        }).WithName("FilterByMajor");

        // Sort by GPA
        app.MapGet($"{route}/sort", ([FromServices] StudentService service, [FromQuery] string order) =>
        {
            return Results.Ok(service.SortByGPA(order));
        }).WithName("SortByGPA");

        // Paginated students
        app.MapGet($"{route}/page", ([FromServices] StudentService service, [FromQuery] int pageNumber, [FromQuery] int pageSize) =>
        {
            var students = service.GetStudentsByPage(pageNumber, pageSize);
            if (!students.Any()) return Results.NotFound("No students found for the given page.");
            return Results.Ok(students);
        }).WithName("PaginateStudents");
    }
}
