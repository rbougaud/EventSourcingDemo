using Application.Services_CQRS.Student.Commands;
using FluentValidation;

namespace Application.Services_CQRS.Student.Validation;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Email).NotEmpty();
        RuleFor(x => x.Dto.FullName).NotEmpty();
    }
}
