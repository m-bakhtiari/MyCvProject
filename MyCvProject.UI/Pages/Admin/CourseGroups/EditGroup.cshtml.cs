using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.UI.Pages.Admin.CourseGroups
{
    public class EditGroupModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroup CourseGroups { get; set; }

        public async Task OnGet(int id)
        {
            CourseGroups = await _courseService.GetById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _courseService.UpdateGroup(CourseGroups);
            return RedirectToPage("Index");
        }
    }
}