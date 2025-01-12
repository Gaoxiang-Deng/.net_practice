namespace MyWebApi.Models;

public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; } // 或使用 string? Name { get; set; }
    public required string Major { get; set; } // 或使用 string? Major { get; set; }
    public double GPA { get; set; }
}