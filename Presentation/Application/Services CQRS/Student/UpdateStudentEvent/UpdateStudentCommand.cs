using Application.Common.Models.Dto.Entities;
using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Services_CQRS.Student.UpdateStudentEvent;

public readonly record struct UpdateStudentCommand(StudentDto Dto) : IRequest<Result<bool, ValidationException>>;
