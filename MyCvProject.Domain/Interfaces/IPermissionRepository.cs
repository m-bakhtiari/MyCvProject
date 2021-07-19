using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;

namespace MyCvProject.Domain.Interfaces
{
    public interface IPermissionRepository
   {
       List<Role> GetRoles();
       int AddRole(Role role);
       Role GetRoleById(int roleId);
       void UpdateRole(Role role);
       void DeleteRole(Role role);
       void AddRolesToUser(List<int> roleIds, int userId);
       void EditRolesUser(int userId, List<int> rolesId);
       List<Permission> GetAllPermission();
       void AddPermissionsToRole(int roleId, List<int> permission);
       List<int> PermissionsRole(int roleId);
       void UpdatePermissionsRole(int roleId, List<int> permissions);
       bool CheckPermission(int permissionId, string userName);
   }
}
