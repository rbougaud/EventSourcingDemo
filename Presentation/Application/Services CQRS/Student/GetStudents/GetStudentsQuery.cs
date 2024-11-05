using MediatR;

namespace Application.Services_CQRS.Student.GetStudents;

public readonly record struct GetStudentsQuery() : IRequest<GetStudentsResponse>;
