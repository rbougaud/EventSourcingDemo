namespace Presentation.Pages.Student.Model;

public class UnEnrolledStudentInputModel
{
    public Guid Id { get; set; }
    public List<Course> AvailableCourses { get; set; } = new List<Course>();
    public List<string> NameCourses { get; set; } = [];
}
