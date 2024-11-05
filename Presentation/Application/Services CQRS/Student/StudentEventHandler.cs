using Application.Common.Mappings;
using Application.Common.Models.Dto.Entities.NotFound;
using Domain.Abstractions.Dto;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Events.Student;
using MediatR;
using Newtonsoft.Json;
using Serilog;

namespace Application.Services_CQRS.Student;

public class StudentEventHandler(ILogger logger, IRepositoryStudentWriter repositoryStudent) : INotificationHandler<Event>
{
    private readonly IRepositoryStudentWriter _Dao = repositoryStudent;
    private readonly ILogger _logger = logger;

    public async Task Handle(Event notification, CancellationToken cancellationToken)
    {
        await Apply(notification);
    }

    public async Task Apply(Event @event)
    {
        switch (@event.Type)
        {
            case nameof(StudentCreated) :
                await ApplyAdd(@event);
                break;
            case nameof(StudentUpdated):
                await ApplyUpdate(@event);
                break;
            case nameof(StudentEnrolled):
                await ApplyEnrolled(@event);
                break;
            case nameof(StudentUnEnrolled):
                await ApplyUnEnrolled(@event);
                break;
            case nameof(StudentDeleted):
                await ApplyDeleted(@event);
                break;
        }
    }

    private async Task ApplyDeleted(Event notification)
    {
        StudentDeleted studentDeleted = JsonConvert.DeserializeObject<StudentDeleted>(notification.Data!)!;
        await _Dao.DeletedAsync(studentDeleted.StudentId);
    }

    private async Task ApplyAdd(Event notification)
    {
        StudentCreated studentCreated = JsonConvert.DeserializeObject<StudentCreated>(notification.Data!)!;
        IStudentDto studentDto = studentCreated.ToDto();
        if (studentDto is not StudentNotFound)
        {
            await _Dao.AddAsync(studentDto);
        }
    }

    private async Task ApplyUpdate(Event notification)
    {
        StudentUpdated studentUpdated = JsonConvert.DeserializeObject<StudentUpdated>(notification.Data!)!;
        IStudentDto studentDto = studentUpdated.ToDto();
        if (studentDto is not StudentNotFound)
        {
            await _Dao.UpdateAsync(studentDto);
        }
    }

    private async Task ApplyEnrolled(Event notification)
    {
        StudentEnrolled studentEnrolled = JsonConvert.DeserializeObject<StudentEnrolled>(notification.Data!)!;
        await _Dao.UpdateEnrolledAsync(studentEnrolled.StudentId, studentEnrolled.CourseName);
    }

    private async Task ApplyUnEnrolled(Event notification)
    {
        StudentUnEnrolled studentUnEnrolled = JsonConvert.DeserializeObject<StudentUnEnrolled>(notification.Data!)!;
        await _Dao.UpdateUnEnrolledAsync(studentUnEnrolled.StudentId, studentUnEnrolled.CourseName);
    }
}
