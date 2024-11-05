using Application.Services_CQRS.Student.Commands;
using Application.Services_CQRS.Student.DeleteStudentEvent;
using Application.Services_CQRS.Student.EnrolledStudentEvent;
using Application.Services_CQRS.Student.UpdateStudentEvent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Presentation.Pages.Student.Model;

public class StudentsPageModel : PageModel
{
    public required List<StudentDto> Students { get; set; }

    [BindProperty]
    public CreateStudentInputModel? Model { get; set; }

    [BindProperty]
    public UpdateStudentInputModel? ModelUpdate { get; set; }

    [BindProperty]
    public EnrollStudentInputModel? EnrollModel { get; set; }

    [BindProperty]
    public UnEnrolledStudentInputModel? UnEnrollModel { get; set; }

    private readonly static Lazy<List<Course>> _AvailableCourses = new(
    [
        new Course { Name = "Anglais" },
        new Course { Name = "Français" },
        new Course { Name = "Histoire" }
    ]);

    public async Task OnGet()
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync("http://localhost:5122/api/StudentQuery/GetStudents");
            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(response);
            Students = apiResponse?.StudentDtos ?? [];
        }

        ViewData["CreateStudentModel"] = new CreateStudentViewModel();
        
        EnrollModel = new EnrollStudentInputModel
        {
            AvailableCourses = _AvailableCourses.Value
        };

        UnEnrollModel = new UnEnrolledStudentInputModel
        {
            AvailableCourses = _AvailableCourses.Value
        };
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        using (var client = new HttpClient())
        {
            try
            {
                var jsonModel = JsonConvert.SerializeObject(new DeleteStudentCommand(id));
                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5122/api/StudentEvent/DeleteStudent", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Student/Students");
                }
                else
                {
                    // Gérer les erreurs si la requête a échoué
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Erreur lors de l'ajout de l'étudiant.");
                    return Page(); // Retourne la page avec les erreurs
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la soumission du formulaire.");
                return Page(); // Retourne la page avec les erreurs
            }
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page(); // Retourne la page avec les erreurs de validation si le modèle n'est pas valide
        }

        using (var client = new HttpClient())
        {
            try
            {
                var dto = new Application.Common.Models.Dto.Entities.StudentDto
                {
                    Id = Guid.NewGuid(),
                    FullName = Model!.FullName!,
                    Email = Model.Email!,
                    DateOfBirth = Model!.DateOfBirth,
                    EnrolledCourses = Model!.EnrolledCourses
                };
                var jsonModel = JsonConvert.SerializeObject(new CreateStudentCommand(dto));
                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5122/api/StudentEvent/CreateStudent", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Student/Students");
                }
                else
                {
                    // Gérer les erreurs si la requête a échoué
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Erreur lors de l'ajout de l'étudiant.");
                    return Page(); // Retourne la page avec les erreurs
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la soumission du formulaire.");
                return Page(); // Retourne la page avec les erreurs
            }
        }
    }

    public async Task<IActionResult> OnPostUpdateStudentAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using (var client = new HttpClient())
        {
            try
            {
                var dto = new Application.Common.Models.Dto.Entities.StudentDto
                {
                    Id = ModelUpdate!.Id,
                    FullName = ModelUpdate.FullName!,
                    Email = ModelUpdate.Email!,
                    DateOfBirth = ModelUpdate.DateOfBirth,
                };
                var jsonModel = JsonConvert.SerializeObject(new UpdateStudentCommand(dto));
                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5122/api/StudentEvent/UpdateStudent", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Student/Students");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Erreur lors de la mise à jour de l'étudiant.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la soumission du formulaire.");
                return Page();
            }
        }
    }

    public async Task<IActionResult> OnPostEnrollStudentAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Guid id = EnrollModel!.Id;
        List<string> names = EnrollModel!.AvailableCourses.Where(c => c.IsSelected).Select(c => c.Name).ToList();
        if (Students is null || Students.Count == 0)
        {
            await OnGet();
        }

        using (var client = new HttpClient())
        {
            try
            {
                var dto = Students!.FirstOrDefault(s => s.Id == id);
                if (dto == null)
                {
                    return RedirectToPage("/Student/Students");
                }
                string? selectedCours = names.FirstOrDefault(c => !dto.EnrolledCourses.Contains(c));
                if (string.IsNullOrEmpty(selectedCours))
                {
                    return RedirectToPage("/Student/Students");
                }

                var jsonModel = JsonConvert.SerializeObject(new EnrolledStudentCommand(dto!.Id, selectedCours));
                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5122/api/StudentEvent/EnrolledStudent", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Student/Students");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Erreur lors de l'ajout des cours.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la soumission du formulaire.");
                return Page();
            }
        }
    }

    public async Task<IActionResult> OnPostUnEnrolledStudentAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Guid id = EnrollModel!.Id;
        List<string> names = EnrollModel!.AvailableCourses.Where(c => c.IsSelected).Select(c => c.Name).ToList();
        if (Students is null || Students.Count == 0)
        {
            await OnGet();
        }

        using (var client = new HttpClient())
        {
            try
            {
                var dto = Students!.FirstOrDefault(s => s.Id == id);
                if (dto == null)
                {
                    return RedirectToPage("/Student/Students");
                }
                string? selectedCours = names.FirstOrDefault(dto.EnrolledCourses.Contains);
                if (string.IsNullOrEmpty(selectedCours))
                {
                    return RedirectToPage("/Student/Students");
                }

                var jsonModel = JsonConvert.SerializeObject(new EnrolledStudentCommand(dto!.Id, selectedCours));
                var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:5122/api/StudentEvent/UnEnrolledStudent", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Student/Students");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Erreur lors de l'ajout des cours.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la soumission du formulaire.");
                return Page();
            }
        }
    }
}

public record ApiResponseDto
{
    public List<StudentDto> StudentDtos { get; set; } = [];
}

public record StudentDto
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public DateTime DateOfBirth { get; init; }
    public List<string> EnrolledCourses { get; init; } = [];
}
