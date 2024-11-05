using Domain.Abstractions.Dto;

namespace Domain.Abstractions.Repositories;

public interface IRepositoryStudentWriter
{
    Task AddAsync(IStudentDto datas);
    Task UpdateAsync(IStudentDto studentDto);
    Task UpdateEnrolledAsync(Guid id, string courseName);
    Task UpdateUnEnrolledAsync(Guid id, string courseName);
    Task DeletedAsync(Guid studentId);
}
