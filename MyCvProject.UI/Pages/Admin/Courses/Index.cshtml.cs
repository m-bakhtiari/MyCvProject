using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels.Course;
using System.Collections.Generic;

namespace MyCvProject.UI.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> ListCourse { get; set; }

        public void OnGet()
        {
            ListCourse = _courseService.GetCoursesForAdmin();
        }
    }
}