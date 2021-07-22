using MyCvProject.Domain.Entities.Permissions;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCvProject.Domain.Interfaces;

namespace MyCvProject.Infra.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly MyCvProjectContext _context;

        public PermissionRepository(MyCvProjectContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<int> AddRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role.RoleId;
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(Role role)
        {
            role.IsDelete = true;
            await UpdateRole(role);
        }

        public async Task AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                await _context.UserRoles.AddAsync(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditRolesUser(int userId, List<int> rolesId)
        {
            //Delete All Roles User
            await _context.UserRoles.Where(r => r.UserId == userId).ForEachAsync(r => _context.UserRoles.Remove(r));

            //Add New Roles
            await AddRolesToUser(rolesId, userId);
        }

        public async Task<List<Permission>> GetAllPermission()
        {
            return await _context.Permission.ToListAsync();
        }

        public async Task AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                await _context.RolePermission.AddAsync(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> PermissionsRole(int roleId)
        {
            return await _context.RolePermission
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToListAsync();
        }

        public async Task UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            await _context.RolePermission.Where(p => p.RoleId == roleId)
                 .ForEachAsync(p => _context.RolePermission.Remove(p));

            await AddPermissionsToRole(roleId, permissions);
        }

        public async Task<bool> CheckPermission(int permissionId, string userName)
        {
            var user = await _context.Users.SingleAsync(u => u.UserName == userName);
            int userId = user.UserId;

            List<int> UserRoles = await _context.UserRoles
                .Where(r => r.UserId == userId).Select(r => r.RoleId).ToListAsync();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = await _context.RolePermission
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToListAsync();

            return RolesPermission.Any(p => UserRoles.Contains(p));

        }
    }
}
