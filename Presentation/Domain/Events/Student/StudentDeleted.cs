using Domain.Entities;

namespace Domain.Events.Student;

public class StudentDeleted : Event
{
    public required Guid StudentId { get; init; }
}
