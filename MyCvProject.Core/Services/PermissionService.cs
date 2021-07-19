using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;
using MyCvProject.Domain.Interfaces;

namespace MyCvProject.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public List<Role> GetRoles()
        {
            return _permissionRepository.GetRoles();
        }

        public int AddRole(Role role)
        {
            return _permissionRepository.AddRole(role);
        }

        public Role GetRoleById(int roleId)
        {
            return _permissionRepository.GetRoleById(roleId);
        }

        public void UpdateRole(Role role)
        {
            _permissionRepository.UpdateRole(role);
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            _permissionRepository.AddRolesToUser(roleIds, userId);
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            _permissionRepository.EditRolesUser(userId, rolesId);
        }

        public List<Permission> GetAllPermission()
        {
            return _permissionRepository.GetAllPermission();
        }

        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            _permissionRepository.AddPermissionsToRole(roleId, permission);
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _permissionRepository.PermissionsRole(roleId);
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _permissionRepository.UpdatePermissionsRole(roleId, permissions);
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            return _permissionRepository.CheckPermission(permissionId, userName);
        }
    }
}
