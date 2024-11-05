using Domain.Abstractions.Dto;

namespace Application.Services_CQRS.Student.GetStudents;

public readonly record struct GetStudentsResponse(ICollection<IStudentDto> StudentDtos);
