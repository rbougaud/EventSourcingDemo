using FluentValidation;

namespace Application.Services_CQRS.Student.UpdateStudentEvent;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Email).NotEmpty();
        RuleFor(x => x.Dto.FullName).NotEmpty();
    }
}
