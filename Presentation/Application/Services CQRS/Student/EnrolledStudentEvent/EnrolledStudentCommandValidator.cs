using FluentValidation;

namespace Application.Services_CQRS.Student.EnrolledStudentEvent;

public class EnrolledStudentCommandValidator : AbstractValidator<EnrolledStudentCommand>
{
    public EnrolledStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).NotNull();
        RuleFor(x => x.CoursName).NotEmpty();
    }
}
