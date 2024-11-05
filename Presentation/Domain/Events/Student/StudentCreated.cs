using Domain.Entities;

namespace Domain.Events.Student;

public class StudentCreated : Event
{
    public required Guid StudentId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required DateTime DateOfBirth { get; init; }
}
