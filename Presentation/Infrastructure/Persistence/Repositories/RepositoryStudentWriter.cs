using Domain.Abstractions.Dto;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryStudentWriter(EventSourcingContext context) : IRepositoryStudentWriter
{
    private readonly EventSourcingContext _context = context;

    public async Task AddAsync(IStudentDto dto)
    {
        var student = new Student
        {
            Id = dto.Id,
            DateOfBirth = dto.DateOfBirth,
            FullName = dto.FullName,
            Email = dto.Email,
            EnrolledCourses = dto.EnrolledCourses
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeletedAsync(Guid studentId)
    {
        var studentToRemove = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
        if (studentToRemove is not null)
        {
            _context.Students.Remove(studentToRemove);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(IStudentDto dto)
    {
        Student? existingStudent = await _context.Students.FindAsync(dto.Id);
        if (existingStudent is not null)
        {
            existingStudent.FullName = dto.FullName;
            existingStudent.Email = dto.Email;
            existingStudent.DateOfBirth = dto.DateOfBirth;

            _context.Entry(existingStudent).Property(s => s.FullName).IsModified = true;
            _context.Entry(existingStudent).Property(s => s.Email).IsModified = true;
            _context.Entry(existingStudent).Property(s => s.DateOfBirth).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateEnrolledAsync(Guid studentId, string courseName)
    {
        Student? student = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
        if (student is not null && !student.EnrolledCourses.Contains(courseName))
        {
            student.EnrolledCourses.Add(courseName);
            _context.Entry(student).Property(s => s.EnrolledCourses).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateUnEnrolledAsync(Guid studentId, string courseName)
    {
        Student? student = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
        if (student is not null && student.EnrolledCourses.Contains(courseName))
        {
            student.EnrolledCourses.Remove(courseName);
            _context.Entry(student).Property(s => s.EnrolledCourses).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
