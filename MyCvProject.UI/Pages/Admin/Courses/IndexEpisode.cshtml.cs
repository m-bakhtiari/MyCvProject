using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes = await _courseService.GetListEpisodeCorse(id);
        }
    }
}