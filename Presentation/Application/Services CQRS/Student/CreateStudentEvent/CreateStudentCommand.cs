using Application.Common.Models.Dto.Entities;
using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Services_CQRS.Student.Commands;

public readonly record struct CreateStudentCommand(StudentDto Dto) : IRequest<Result<bool, ValidationException>>;

