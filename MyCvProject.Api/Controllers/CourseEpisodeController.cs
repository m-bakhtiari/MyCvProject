using Microsoft.AspNetCore.Mvc;
using MyCvProject.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Mapper;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.ViewModels.Course;

namespace MyCvProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEpisodeController : ControllerBase
    {
        #region Ctor

        private readonly ICourseService _courseService;

        public CourseEpisodeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        /// <summary>
        /// گرفتن تمام لیست قسمت ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CourseEpisode>))]
        public async Task<IActionResult> GetCourseEpisodes()
        {
            var courseEpisodes = await _courseService.GetAllEpisode();
            return Ok(courseEpisodes);
        }

        /// <summary>
        /// گرفتن بخش با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CourseEpisode))]
        public async Task<IActionResult> GetCourseEpisodeById(int id)
        {
            var courseEpisode = await _courseService.GetEpisodeById(id);
            return Ok(courseEpisode);
        }

        /// <summary>
        /// حذف بخش با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCourseEpisodeById(int id)
        {
            await _courseService.DeleteEpisode(id);
            return Ok();
        }

        /// <summary>
        /// افزودن بخش جدید
        /// </summary>
        /// <param name="courseEpisodeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddCourseEpisode(CourseEpisodeViewModel courseEpisodeViewModel)
        {
            var courseEpisode = courseEpisodeViewModel.ToCourseEpisode();
            var result = await _courseService.AddEpisode(courseEpisode);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// ویرایش بخش با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseEpisodeViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCourseEpisode(int id, CourseEpisodeViewModel courseEpisodeViewModel)
        {
            if (courseEpisodeViewModel.EpisodeId != id)
            {
                return BadRequest();
            }
            var courseEpisode = courseEpisodeViewModel.ToCourseEpisode();
            var result = await _courseService.AddEpisode(courseEpisode);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }
    }
}
