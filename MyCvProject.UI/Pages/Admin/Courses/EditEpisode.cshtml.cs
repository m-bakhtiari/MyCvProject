using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;

namespace MyCvProject.UI.Pages.Admin.Courses
{
    public class EditEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public async Task OnGet(int id)
        {
            CourseEpisode = await _courseService.GetEpisodeById(id);
        }

        public async Task<IActionResult> OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid)
                return Page();

            if (fileEpisode != null)
            {
                if (_courseService.CheckExistFile(fileEpisode.FileName))
                {
                    ViewData["IsExistFile"] = true;
                    return Page();
                }
            }
            await _courseService.EditEpisode(CourseEpisode, fileEpisode);

            return Redirect("/Admin/Courses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}