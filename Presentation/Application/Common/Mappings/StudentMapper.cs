using Application.Common.Models.Dto.Entities;
using Application.Common.Models.Dto.Entities.NotFound;
using Domain.Abstractions.Dto;
using Domain.Entities;
using Domain.Events.Student;

namespace Application.Common.Mappings;

public static class StudentMapper
{
    public static IStudentDto ToDto(this Student? student)
    {
        if (student is null) { return new StudentNotFound(); }
        return new StudentDto
        {
            Id = student.Id,
            Email = student.Email,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            EnrolledCourses = student.EnrolledCourses
        };
    }

    public static IStudentDto ToDto(this StudentCreated? student)
    {
        if (student is null) { return new StudentNotFound(); }
        return new StudentDto
        {
            Id = student.StudentId,
            Email = student.Email,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            EnrolledCourses = []
        };
    }

    public static IStudentDto ToDto(this StudentUpdated? student)
    {
        if (student is null) { return new StudentNotFound(); }
        return new StudentDto
        {
            Id = student.StudentId,
            Email = student.Email,
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            EnrolledCourses = student.EnrolledCourses
        };
    }
}
