using Application.Common.Mappings;
using Domain.Abstractions.Dto;
using Domain.Abstractions.Repositories;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.GetStudents;

public class GetStudentsHandler(ILogger logger, IRepositoryStudentReader repositoryStudent) : IRequestHandler<GetStudentsQuery, GetStudentsResponse>
{
    private readonly IRepositoryStudentReader _Dao = repositoryStudent;
    private readonly ILogger _logger = logger;

    public async Task<GetStudentsResponse> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(GetStudentsHandler));
        ICollection<IStudentDto> students = _Dao.GetStudents().Select(x => x.ToDto()).ToList();
        return new GetStudentsResponse(students);
    }
}
