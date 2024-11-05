using MediatR;

namespace Application.Services_CQRS.Student.DeleteStudentEvent;

public readonly record struct DeleteStudentCommand(Guid Id) : IRequest<bool>;
