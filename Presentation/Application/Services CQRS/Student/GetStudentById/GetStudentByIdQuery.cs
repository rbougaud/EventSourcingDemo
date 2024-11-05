using MediatR;

namespace Application.Services_CQRS.Student.GetStudentById;

public readonly record struct GetStudentByIdQuery(Guid Id) : IRequest<GetStudentByIdResponse>;
