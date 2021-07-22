using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<Role>> GetRoles();
        Task<int> AddRole(Role role);
        Task<Role> GetRoleById(int roleId);
        Task UpdateRole(Role role);
        Task DeleteRole(Role role);
        Task AddRolesToUser(List<int> roleIds, int userId);
        Task EditRolesUser(int userId, List<int> rolesId);
        Task<List<Permission>> GetAllPermission();
        Task AddPermissionsToRole(int roleId, List<int> permission);
        Task<List<int>> PermissionsRole(int roleId);
        Task UpdatePermissionsRole(int roleId, List<int> permissions);
        Task<bool> CheckPermission(int permissionId, string userName);
    }
}
