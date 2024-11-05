using Domain.Entities;

namespace Domain.Events.Student;

public class StudentUpdated : Event
{
    public required Guid StudentId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public List<string> EnrolledCourses { get; init; } = [];
}
