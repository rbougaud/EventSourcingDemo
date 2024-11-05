using Application.Services_CQRS.Student.Commands;
using Domain.Common;
using Domain.Events.Student;
using FluentValidation;
using Infrastructure.Abstractions.Stores;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.Eventhandlers;

public class CreateStudentEventPublisher(ILogger logger, IEventStore eventStore, IValidator<CreateStudentCommand> validator) : IRequestHandler<CreateStudentCommand, Result<bool, ValidationException>>
{
    private readonly ILogger _logger = logger;
    private readonly IEventStore _eventStore = eventStore;
    private readonly IValidator<CreateStudentCommand> _validator = validator;

    public async Task<Result<bool, ValidationException>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(CreateStudentEventPublisher));
        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            var s = new StudentCreated
            {
                Type = nameof(StudentCreated),
                DateOfBirth = request.Dto.DateOfBirth,
                Email = request.Dto.Email,
                FullName = request.Dto.FullName,
                StudentId = request.Dto.Id,
            };
            return await _eventStore.AppendAsync(s);
        }
        return new ValidationException(validationResult.Errors);
    }
}
