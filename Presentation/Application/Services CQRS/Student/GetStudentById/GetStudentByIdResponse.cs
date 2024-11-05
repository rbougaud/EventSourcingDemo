using Domain.Abstractions.Dto;

namespace Application.Services_CQRS.Student.GetStudentById;

public readonly record struct GetStudentByIdResponse(IStudentDto StudentDto);
