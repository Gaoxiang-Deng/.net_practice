namespace SchoolManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Grade> Grades { get; set; } = new();
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
    }

    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public double Score { get; set; }
    }
}