namespace Presentation.Pages.Student.Model;

public class EnrollStudentInputModel
{
    public Guid Id { get; set; }
    public List<Course> AvailableCourses { get; set; } = [];
}

public class Course
{
    public required string Name { get; set; }
    public bool IsSelected { get; set; }
}
