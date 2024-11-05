using Application.Common.Mappings;
using Domain.Abstractions.Repositories;
using MediatR;
using Serilog;

namespace Application.Services_CQRS.Student.GetStudentById;

public class GetStudentByIdHandler(ILogger logger, IRepositoryStudentReader repositoryStudent) : IRequestHandler<GetStudentByIdQuery, GetStudentByIdResponse>
{
    private readonly Lazy<IRepositoryStudentReader> _lazyDao = new(repositoryStudent);
    private readonly ILogger _logger = logger;

    public async Task<GetStudentByIdResponse> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.Information(nameof(GetStudentByIdHandler));
        //On cast en objet réél pour instancier lors de la désérialisation
        var student = await _lazyDao.Value.GetStudentByIdQuery(request.Id);
        return new GetStudentByIdResponse(student.ToDto());
    }
}
