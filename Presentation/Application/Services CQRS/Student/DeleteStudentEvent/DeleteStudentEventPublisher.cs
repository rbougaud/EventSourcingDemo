using Domain.Events.Student;
using Infrastructure.Abstractions.Stores;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.DeleteStudentEvent;

public class DeleteStudentEventPublisher(ILogger logger, IEventStore eventStore) : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly ILogger _logger = logger;
    private readonly IEventStore _eventStore = eventStore;

    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(DeleteStudentEventPublisher));
        var s = new StudentDeleted
        {
            Type = nameof(StudentDeleted),
            StudentId = request.Id
        };
        return await _eventStore.AppendAsync(s);
    }
}
