using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<List<Role>> GetRoles()
        {
            return await _permissionRepository.GetRoles();
        }

        public async Task<int> AddRole(Role role)
        {
            return await _permissionRepository.AddRole(role);
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _permissionRepository.GetRoleById(roleId);
        }

        public async Task UpdateRole(Role role)
        {
            await _permissionRepository.UpdateRole(role);
        }

        public async Task DeleteRole(Role role)
        {
            role.IsDelete = true;
            await UpdateRole(role);
        }

        public async Task AddRolesToUser(List<int> roleIds, int userId)
        {
            await _permissionRepository.AddRolesToUser(roleIds, userId);
        }

        public async Task EditRolesUser(int userId, List<int> rolesId)
        {
            await _permissionRepository.EditRolesUser(userId, rolesId);
        }

        public async Task<List<Permission>> GetAllPermission()
        {
            return await _permissionRepository.GetAllPermission();
        }

        public async Task AddPermissionsToRole(int roleId, List<int> permission)
        {
            await _permissionRepository.AddPermissionsToRole(roleId, permission);
        }

        public async Task<List<int>> PermissionsRole(int roleId)
        {
            return await _permissionRepository.PermissionsRole(roleId);
        }

        public async Task UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            await _permissionRepository.UpdatePermissionsRole(roleId, permissions);
        }

        public async Task<bool> CheckPermission(int permissionId, string userName)
        {
            return await _permissionRepository.CheckPermission(permissionId, userName);
        }
    }
}
