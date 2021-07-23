using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.UI.Pages.Admin.CourseGroups
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseGroup> CourseGroups { get; set; }
        public async Task OnGet()
        {
            CourseGroups =await _courseService.GetAllGroup();
        }
    }
}