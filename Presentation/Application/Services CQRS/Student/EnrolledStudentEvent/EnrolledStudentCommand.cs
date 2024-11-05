using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Services_CQRS.Student.EnrolledStudentEvent;

public readonly record struct EnrolledStudentCommand(Guid StudentId, string CoursName) : IRequest<Result<bool, ValidationException>>;
