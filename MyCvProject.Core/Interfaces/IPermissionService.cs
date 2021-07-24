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
        Task AddRolesToUser(List<int> roleIds, int userId);
        Task EditRolesUser(int userId, List<int> rolesId);

        #endregion

        #region Permission

        Task<List<Permission>> GetAllPermission();
        Task AddPermissionsToRole(int roleId, List<int> permission);
        Task<List<int>> PermissionsRole(int roleId);
        Task UpdatePermissionsRole(int roleId, List<int> permissions);
        Task<bool> CheckPermission(int permissionId, string userName);

        #endregion
    }
}
