using Domain.Abstractions.Dto;

namespace Application.Common.Models.Dto.Entities;

public record StudentDto : IStudentDto
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public DateTime DateOfBirth { get; init; }
    public List<string> EnrolledCourses { get; init; } = [];
}
