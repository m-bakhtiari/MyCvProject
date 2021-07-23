using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.UI.Pages.Admin.Courses
{
    public class CreateEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = new CourseEpisode
            {
                CourseId = id
            };
        }

        public async Task<IActionResult> OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid || fileEpisode == null)
                return Page();

            if (_courseService.CheckExistFile(fileEpisode.FileName))
            {
                ViewData["IsExistFile"] = true;
                return Page();
            }

            await _courseService.AddEpisode(CourseEpisode, fileEpisode);
            return Redirect("/Admin/Courses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}