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
    public class CourseGroupController : ControllerBase
    {
        #region Ctor

        private readonly ICourseService _courseService;

        public CourseGroupController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        /// <summary>
        /// گرفتن تمام لیست دسته بندی های دوره
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CourseGroup>))]
        public async Task<IActionResult> GetCourseGroups()
        {
            var course = await _courseService.GetAllGroup();
            return Ok(course);
        }

        /// <summary>
        /// گرفتن دسته بندی دوره با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CourseGroup))]
        public async Task<IActionResult> GetCourseGroupById(int id)
        {
            var courseGroup = await _courseService.GetCourseGroupById(id);
            return Ok(courseGroup);
        }

        /// <summary>
        /// حذف دسته بندی گروه با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCourseGroupById(int id)
        {
            await _courseService.DeleteGroup(id);
            return Ok();
        }

        /// <summary>
        /// افزودن گروه بندی دوره جدید
        /// </summary>
        /// <param name="courseGroupViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCourseGroup(CourseGroupViewModel courseGroupViewModel)
        {
            var courseGroup = courseGroupViewModel.ToCourseGroup();
            var result = await _courseService.AddGroup(courseGroup);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// ویرایش گروه بندی دوره با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseGroupViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCourseGroup(int id, CourseGroupViewModel courseGroupViewModel)
        {
            if (courseGroupViewModel.GroupId != id)
            {
                return BadRequest();
            }
            var courseGroup = courseGroupViewModel.ToCourseGroup();
            var result = await _courseService.UpdateGroup(courseGroup);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }
    }
}
