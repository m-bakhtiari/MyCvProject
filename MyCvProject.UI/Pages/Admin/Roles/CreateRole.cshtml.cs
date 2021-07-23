using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.UI.Pages.Admin.Roles
{
    [PermissionChecker(1003)]
    public class CreateRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        [BindProperty]
        public Role Role { get; set; }

        public async Task OnGet()
        {
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

            Role.IsDelete = false;
            int roleId = await _permissionService.AddRole(Role);

            await _permissionService.AddPermissionsToRole(roleId, SelectedPermission);

            return RedirectToPage("Index");
        }
    }
}