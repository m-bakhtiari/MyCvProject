using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.UI.Pages.Admin.CourseGroups
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
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
            CourseGroups = await _courseService.GetCourseGroupById(id);
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