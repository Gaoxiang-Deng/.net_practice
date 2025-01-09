namespace MyWebApi.Models;

public record Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Major { get; set; }
    public double GPA { get; set; }
}