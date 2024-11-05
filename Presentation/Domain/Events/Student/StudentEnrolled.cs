using Domain.Entities;

namespace Domain.Events.Student;

public class StudentEnrolled : Event
{
    public required Guid StudentId { get; init; }
    public required string CourseName { get; init; }
}
