using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;

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
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();


            _permissionService.UpdateRole(Role);

            _permissionService.UpdatePermissionsRole(Role.RoleId,SelectedPermission);

            return RedirectToPage("Index");
        }
    }
}