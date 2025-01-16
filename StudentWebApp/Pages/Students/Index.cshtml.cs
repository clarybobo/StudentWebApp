using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentWebApp.Data.Interfaces;
using StudentWebApp.Data.Models;

namespace StudentWebApp.Pages.Students
{
    public class IndexModel(IStudentService studentService) : PageModel
    {
        private readonly IStudentService studentService = studentService;

        public List<Student> Students { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Students = await studentService.GetStudentsAsync();
        }
    }
}
