using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Core.Mapper;
using MyCvProject.Domain.ViewModels.Course;

namespace MyCvProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        #region Ctor

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        /// <summary>
        /// گرفتن تمام لیست دوره ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Course>))]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCourse();
            return Ok(courses);
        }

        /// <summary>
        /// گرفتن دوره با آیدی
        /// </summary>
        /// <param name="id">آیدی دوره</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            return Ok(course);
        }

        /// <summary>
        /// حذف دوره با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCourseById(int id)
        {
            await _courseService.DeleteCourse(id);
            return Ok();
        }

        /// <summary>
        /// افزودن دوره جدید
        /// </summary>
        /// <param name="courseViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCourse(CourseViewModel courseViewModel)
        {
            var course = courseViewModel.ToCourse();
            var result = await _courseService.AddCourse(course);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// ویرایش دوره با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCourse(int id, CourseViewModel courseViewModel)
        {
            if (courseViewModel.CourseId != id)
            {
                return BadRequest();
            }
            var course = courseViewModel.ToCourse();
            var result = await _courseService.UpdateCourse(course);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }
    }
}
