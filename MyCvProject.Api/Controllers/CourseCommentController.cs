using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Mapper;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.ViewModels;
using MyCvProject.Domain.ViewModels.User;

namespace MyCvProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCommentController : ControllerBase
    {
        #region Ctor

        private readonly IUserService _userService;

        public CourseCommentController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        /// <summary>
        /// گرفتن تمام لیست کاربران
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        /// <summary>
        /// حذف کاربر با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        /// <summary>
        /// افزودن کاربر جدید
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddUser(UserViewModel userViewModel)
        {
            var user = userViewModel.ToUser();
            var result = await _userService.AddUser(user);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// ویرایش کاربر با آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateUser(int id, UserViewModel userViewModel)
        {
            if (userViewModel.UserId != id)
            {
                return BadRequest();
            }
            var user = userViewModel.ToUser();
            var result = await _userService.AddUser(user);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }
    }
}
