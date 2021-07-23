using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.UI.Pages.Admin.CourseGroups
{
    [PermissionChecker(Const.PermissionIdForAdmin)]
    public class CreateGroupModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroup CourseGroups { get; set; }

        public void OnGet(int? id)
        {
            CourseGroups = new CourseGroup()
            {
                ParentId = id
            };
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _courseService.AddGroup(CourseGroups);

            return RedirectToPage("Index");
        }
    }
}