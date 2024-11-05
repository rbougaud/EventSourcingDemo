using FluentValidation;

namespace Application.Services_CQRS.Student.UnEnrolledStudentEvent;

public class UnEnrolledStudentCommandValidator : AbstractValidator<UnEnrolledStudentCommand>
{
    public UnEnrolledStudentCommandValidator()
    {
        RuleFor(x => x.StudentId).NotNull();
        RuleFor(x => x.CoursName).NotEmpty();
    }
}
