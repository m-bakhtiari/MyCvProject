using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels.Course;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Courses
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> ListCourse { get; set; }

        public async Task OnGet()
        {
            ListCourse = await _courseService.GetCoursesForAdmin();
        }
    }
}