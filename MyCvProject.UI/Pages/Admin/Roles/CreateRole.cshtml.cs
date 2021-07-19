using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;

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

        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermission();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

           
            Role.IsDelete = false;
            int roleId = _permissionService.AddRole(Role);

            _permissionService.AddPermissionsToRole(roleId,SelectedPermission);

            return RedirectToPage("Index");
        }
    }
}