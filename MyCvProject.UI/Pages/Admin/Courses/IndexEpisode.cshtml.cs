using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using System.Collections.Generic;

namespace MyCvProject.UI.Pages.Admin.Courses
{
    public class IndexEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes = _courseService.GetListEpisodeCorse(id);
        }
    }
}