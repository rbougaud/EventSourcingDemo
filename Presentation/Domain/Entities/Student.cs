namespace Domain.Entities;

public class Student
{
    public Guid Id { get; init; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> EnrolledCourses { get; set; } = [];
}
