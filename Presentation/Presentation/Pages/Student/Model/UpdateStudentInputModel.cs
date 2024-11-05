namespace Presentation.Pages.Student.Model;

public class UpdateStudentInputModel
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
