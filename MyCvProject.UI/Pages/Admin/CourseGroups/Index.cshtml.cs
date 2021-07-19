using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using System.Collections.Generic;

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
        public void OnGet()
        {
            CourseGroups = _courseService.GetAllGroup();
        }
    }
}