using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

public static class StudentController
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        var route = "/students";

        app.MapGet(route, (StudentService service, int? page, int? size) =>
        {
            var students = service.GetStudents(page, size);
            return Results.Ok(students);
        }).WithName("GetAllStudents");

        app.MapPost(route, (StudentService service, Student student) =>
        {
            service.AddStudent(student);
            return Results.Created($"{route}/{student.Id}", student);
        }).WithName("CreateStudent");

        app.MapPut($"{route}/{{id}}", (StudentService service, int id, Student updatedStudent) =>
        {
            if (service.UpdateStudent(id, updatedStudent))
            {
                return Results.NoContent();
            }
            return Results.NotFound();
        }).WithName("UpdateStudent");

        app.MapDelete($"{route}/{{id}}", (StudentService service, int id) =>
        {
            if (service.DeleteStudent(id))
            {
                return Results.NoContent();
            }
            return Results.NotFound();
        }).WithName("DeleteStudent");
    }
}