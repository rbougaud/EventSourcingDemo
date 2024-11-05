﻿namespace Presentation.Pages.Student.Model;

public class UpdateStudentViewModel
{
    public Guid StudentId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    //public List<string> EnrolledCourses { get; set; } = [];
}
