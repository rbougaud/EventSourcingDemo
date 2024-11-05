using Application.Services_CQRS.Student.GetStudentById;
using Application.Services_CQRS.Student.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Student;

[ApiController]
[Route("api/[controller]")]
public class StudentQueryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("[action]")]
    public async Task<IActionResult> GetStudents()
    {
        var query = new GetStudentsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetStudentById(GetStudentByIdRequest request)
    {
        var query = new GetStudentByIdQuery(request.Id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
