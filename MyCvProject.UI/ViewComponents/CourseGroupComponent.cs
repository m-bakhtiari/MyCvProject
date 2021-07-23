using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using System.Threading.Tasks;

namespace MyCvProject.UI.ViewComponents
{
    public class CourseGroupComponent : ViewComponent
    {
        private readonly ICourseService _courseService;

        public CourseGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("CourseGroup",await _courseService.GetAllGroup()));
        }
    }
}
