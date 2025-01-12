using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

public static class StudentController
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        var route = "/students";

        // 查询学生列表（支持分页、筛选、排序）
        app.MapGet(route, ([FromServices] StudentService service, [FromQuery] string? major, [FromQuery] string? order, [FromQuery] int? page, [FromQuery] int? size) =>
        {
            var students = service.GetAllStudents();

            // 筛选
            if (!string.IsNullOrEmpty(major))
            {
                students = service.FilterByMajor(major);
            }

            // 排序
            if (!string.IsNullOrEmpty(order))
            {
                students = service.SortByGPA(order);
            }

            // 分页
            if (page.HasValue && size.HasValue)
            {
                students = service.GetStudentsByPage(students, page.Value, size.Value);
            }

            return Results.Ok(students);
        }).WithName("GetStudents");

        // 查询单个学生
        app.MapGet($"{route}/{{id}}", ([FromServices] StudentService service, int id) =>
        {
            var student = service.GetStudentById(id);
            if (student == null) return Results.NotFound($"Student with ID {id} not found.");
            return Results.Ok(student);
        }).WithName("GetStudentById");

        // 添加学生
        app.MapPost(route, ([FromServices] StudentService service, [FromBody] Student student) =>
        {
            service.AddStudent(student);
            return Results.Created($"{route}/{student.Id}", student);
        }).WithName("AddStudent");

        // 更新学生
        app.MapPut($"{route}/{{id}}", ([FromServices] StudentService service, int id, [FromBody] Student updatedStudent) =>
        {
            if (!service.UpdateStudent(id, updatedStudent))
            {
                return Results.NotFound($"Student with ID {id} not found.");
            }
            return Results.Ok(updatedStudent);
        }).WithName("UpdateStudent");

        // 删除学生
        app.MapDelete($"{route}/{{id}}", ([FromServices] StudentService service, int id) =>
        {
            if (!service.DeleteStudent(id))
            {
                return Results.NotFound($"Student with ID {id} not found.");
            }
            return Results.Ok($"Student with ID {id} deleted successfully.");
        }).WithName("DeleteStudent");
    }
}
