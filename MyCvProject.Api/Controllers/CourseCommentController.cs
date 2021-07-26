using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Mapper;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.ViewModels;
using MyCvProject.Domain.ViewModels.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCommentController : ControllerBase
    {
        #region Ctor

        private readonly ICourseService _courseService;

        public CourseCommentController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        /// <summary>
        /// گرفتن کامنت های یک دوره با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(List<CourseComment>))]
        public async Task<IActionResult> GetCourseCommentById(int id)
        {
            var comments = await _courseService.GetCourseComment(id);
            return Ok(comments);
        }

        /// <summary>
        /// افزودن کامنت جدید
        /// </summary>
        /// <param name="courseCommentViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCourseComment(CourseCommentViewModel courseCommentViewModel)
        {
            var courseComment = courseCommentViewModel.ToCourseComment();
            var result = await _courseService.AddComment(courseComment, User.Identity.Name);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }
    }
}
