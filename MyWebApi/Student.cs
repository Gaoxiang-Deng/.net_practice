namespace MyWebApi.Models;

public record Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}