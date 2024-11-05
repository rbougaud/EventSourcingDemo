namespace Domain.Abstractions.Dto;

public interface IStudentDto
{
    Guid Id { get; init; }
    string FullName { get; init; }
    string Email { get; init; }
    DateTime DateOfBirth { get; init; }
    List<string> EnrolledCourses { get; init; }
}
