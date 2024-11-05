using Domain.Common;
using Domain.Events.Student;
using FluentValidation;
using Infrastructure.Abstractions.Stores;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.UnEnrolledStudentEvent;

public class UnEnrolledStudentEventPublisher(ILogger logger, IEventStore eventStore, IValidator<UnEnrolledStudentCommand> validator) : IRequestHandler<UnEnrolledStudentCommand, Result<bool, ValidationException>>
{
    private readonly ILogger _logger = logger;
    private readonly IEventStore _eventStore = eventStore;
    private readonly IValidator<UnEnrolledStudentCommand> _validator = validator;

    public async Task<Result<bool, ValidationException>> Handle(UnEnrolledStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(UnEnrolledStudentEventPublisher));
        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            var s = new StudentUnEnrolled
            {
                Type = nameof(StudentUnEnrolled),
                StudentId = request.StudentId,
                CourseName = request.CoursName
            };
            return await _eventStore.AppendAsync(s);
        }
        return new ValidationException(validationResult.Errors);
    }
}
