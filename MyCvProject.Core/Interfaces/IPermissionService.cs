using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.ViewModels;

namespace MyCvProject.Core.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        /// <summary>
        /// گرفتن لیست نقش ها
        /// </summary>
        /// <returns></returns>
        Task<List<Role>> GetRoles();

        /// <summary>
        /// افزودن نقش جدید
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<OpRes<int>> AddRole(Role role);

        /// <summary>
        /// گرفتن نقش با آیدی
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<Role> GetRoleById(int roleId);

        /// <summary>
        /// ویرایش نقش
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<OpRes> UpdateRole(Role role);

        /// <summary>
        /// حذف نقش
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task DeleteRole(Role role);

        /// <summary>
        /// حذف نقش با آیدی
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task DeleteRole(int roleId);

        /// <summary>
        /// اقزودن نقش جدید به کاربر
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AddRolesToUser(List<int> roleIds, int userId);

        /// <summary>
        /// ویرایش نقش برای گاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rolesId"></param>
        /// <returns></returns>
        Task EditRolesUser(int userId, List<int> rolesId);

        #endregion

        #region Permission

        /// <summary>
        /// گرفتن تمام سطح های دسترسی
        /// </summary>
        /// <returns></returns>
        Task<List<Permission>> GetAllPermission();

        /// <summary>
        /// افزودن سطح دسترسی به نقش 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task AddPermissionsToRole(int roleId, List<int> permission);

        /// <summary>
        /// گرفتن سطح دسترسی یک نقش
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<int>> PermissionsRole(int roleId);

        /// <summary>
        /// ویرایش سطح های دسترسی یک نقش
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task UpdatePermissionsRole(int roleId, List<int> permissions);

        /// <summary>
        /// وضعیت امکان دسترسی یک کاربر
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> CheckPermission(int permissionId, string userName);

        #endregion
    }
}
