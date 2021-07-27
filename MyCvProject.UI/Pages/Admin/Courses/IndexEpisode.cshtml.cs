using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Courses
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
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