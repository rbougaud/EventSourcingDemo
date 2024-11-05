using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Services_CQRS.Student.UnEnrolledStudentEvent;

public readonly record struct UnEnrolledStudentCommand(Guid StudentId, string CoursName) : IRequest<Result<bool, ValidationException>>;
