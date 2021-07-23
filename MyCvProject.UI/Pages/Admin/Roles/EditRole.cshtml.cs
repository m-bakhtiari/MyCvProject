using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.UI.Pages.Admin.Roles
{
    [PermissionChecker(1004)]
    public class EditRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public async Task OnGet(int id)
        {
            Role = await _permissionService.GetRoleById(id);
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = await _permissionService.PermissionsRole(id);
        }

        public async Task<IActionResult> OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

            await _permissionService.UpdateRole(Role);
            await _permissionService.UpdatePermissionsRole(Role.RoleId, SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}