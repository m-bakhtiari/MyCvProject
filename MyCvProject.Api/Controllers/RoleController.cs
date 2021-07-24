using Microsoft.AspNetCore.Mvc;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region Ctor

        private readonly IPermissionService _permissionService;

        public RoleController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion

        /// <summary>
        /// گرفتن لیست نقش ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Role>))]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _permissionService.GetRoles();
            return Ok(roles);
        }

        /// <summary>
        /// گرفتن یک نقش با آیدی
        /// </summary>
        /// <param name="id">آیدی نقش مورد نظر</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _permissionService.GetRoleById(id);
            return Ok(role);
        }

        /// <summary>
        /// افزودن نقش جدید
        /// </summary>
        /// <param name="role">نقش</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddRole(Role role)
        {
            var result = await _permissionService.AddRole(role);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// ویرایش نقش یا آیدی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateRole(int id, Role role)
        {
            if (role.RoleId != id)
            {
                return BadRequest();
            }
            var result = await _permissionService.UpdateRole(role);
            if (result.IsSuccess == false)
            {
                return result.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// حذف نقش با آیدی
        /// </summary>
        /// <param name="id">آیدی نقش</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _permissionService.DeleteRole(id);
            return Ok();
        }
    }
}
