using Application.Services_CQRS.Student.Commands;
using Application.Services_CQRS.Student.DeleteStudentEvent;
using Application.Services_CQRS.Student.EnrolledStudentEvent;
using Application.Services_CQRS.Student.UnEnrolledStudentEvent;
using Application.Services_CQRS.Student.UpdateStudentEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Student;

[ApiController]
[Route("api/[controller]")]
public class StudentEventController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("[action]")]
    public async Task<bool> DeleteStudent([FromBody] DeleteStudentCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand request)
    {
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>
               (
                   v => Ok(v),
                   failed => BadRequest(failed)
               );
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentCommand request)
    {
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>
               (
                   v => Ok(v),
                   failed => BadRequest(failed)
               );
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> EnrolledStudent([FromBody] EnrolledStudentCommand request)
    {
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>
               (
                   v => Ok(v),
                   failed => BadRequest(failed)
               );
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UnEnrolledStudent([FromBody] UnEnrolledStudentCommand request)
    {
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>
               (
                   v => Ok(v),
                   failed => BadRequest(failed)
               );
    }
}
