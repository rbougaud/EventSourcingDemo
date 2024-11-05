using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryStudentReader(ReadModelContext context) : IRepositoryStudentReader
{
    private readonly ReadModelContext _context = context;

    public async Task<Student?> GetStudentByIdQuery(Guid id)
    {
        return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IEnumerable<Student> GetStudents()
    {
        return _context.Students;
    }
}
