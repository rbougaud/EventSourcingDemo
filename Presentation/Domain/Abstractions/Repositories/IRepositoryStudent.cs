using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IRepositoryStudentReader
{
    Task<Student?> GetStudentByIdQuery(Guid id);
    IEnumerable<Student> GetStudents();
}
