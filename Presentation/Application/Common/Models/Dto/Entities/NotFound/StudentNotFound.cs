using Domain.Abstractions.Dto;

namespace Application.Common.Models.Dto.Entities.NotFound;

internal class StudentNotFound : IStudentDto
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = "Not Found";
    public string Email { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public List<string> EnrolledCourses { get; init; } = [];
}
