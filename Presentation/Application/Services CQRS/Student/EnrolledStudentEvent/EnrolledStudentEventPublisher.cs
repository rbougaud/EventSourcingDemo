using Domain.Common;
using Domain.Events.Student;
using FluentValidation;
using Infrastructure.Abstractions.Stores;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.EnrolledStudentEvent;

public class EnrolledStudentEventPublisher(ILogger logger, IEventStore eventStore, IValidator<EnrolledStudentCommand> validator) : IRequestHandler<EnrolledStudentCommand, Result<bool, ValidationException>>
{
    private readonly ILogger _logger = logger;
    private readonly IEventStore _eventStore = eventStore;
    private readonly IValidator<EnrolledStudentCommand> _validator = validator;

    public async Task<Result<bool, ValidationException>> Handle(EnrolledStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(EnrolledStudentEventPublisher));
        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            var s = new StudentEnrolled
            {
                Type = nameof(StudentEnrolled),
                StudentId = request.StudentId,
                CourseName = request.CoursName
            };
            return await _eventStore.AppendAsync(s);
        }
        return new ValidationException(validationResult.Errors);
    }
}
