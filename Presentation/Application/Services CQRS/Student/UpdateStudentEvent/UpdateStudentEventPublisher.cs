using Application.Services_CQRS.Student.Eventhandlers;
using Domain.Common;
using Domain.Events.Student;
using FluentValidation;
using Infrastructure.Abstractions.Stores;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.UpdateStudentEvent;

public class UpdateStudentEventPublisher(ILogger logger, IEventStore eventStore, IValidator<UpdateStudentCommand> validator) : IRequestHandler<UpdateStudentCommand, Result<bool, ValidationException>>
{
    private readonly ILogger _logger = logger;
    private readonly IEventStore _eventStore = eventStore;
    private readonly IValidator<UpdateStudentCommand> _validator = validator;

    public async Task<Result<bool, ValidationException>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(CreateStudentEventPublisher));
        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            var s = new StudentUpdated
            {
                Type = nameof(StudentUpdated),
                DateOfBirth = request.Dto.DateOfBirth,
                Email = request.Dto.Email,
                FullName = request.Dto.FullName,
                StudentId = request.Dto.Id
            };
            return await _eventStore.AppendAsync(s);
        }
        return new ValidationException(validationResult.Errors);
    }
}
