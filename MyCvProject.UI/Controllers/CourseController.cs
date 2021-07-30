using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyCvProject.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public CourseController(ICourseService courseService, IOrderService orderService, IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups =await _courseService.GetAllGroup();
            ViewBag.pageId = pageId;
            return View(await _courseService.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9));
        }


        [Route("ShowCourse/{id}")]
        public async Task<IActionResult> ShowCourse(int id)
        {
            var course = await _courseService.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [Authorize]
        public async Task<ActionResult> BuyCourse(int id)
        {
            int orderId = await _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId);
        }

        [Route("DownloadFile/{episodeId}")]
        public async Task<IActionResult> DownloadFile(int episodeId)
        {
            var episode = await _courseService.GetEpisodeById(episodeId);
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",
                episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                byte[] file = await System.IO.File.ReadAllBytesAsync(filepath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (await _orderService.IsUserInCourse(User.Identity.Name, episode.CourseId))
                {
                    byte[] file = await System.IO.File.ReadAllBytesAsync(filepath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return Forbid();
        }



        [HttpPost]
        public async Task<IActionResult> CreateComment(CourseComment comment)
        {
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = await _userService.GetUserIdByUserName(User.Identity.Name);
            await _courseService.AddComment(comment);

            return View("ShowComment", await _courseService.GetCourseComment(comment.CourseId, 1));
        }

        public async Task<IActionResult> ShowComment(int id, int pageId = 1)
        {
            return View(await _courseService.GetCourseComment(id, pageId));
        }
    }
}