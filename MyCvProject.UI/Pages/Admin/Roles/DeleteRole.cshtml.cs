using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;

namespace MyCvProject.UI.Pages.Admin.Roles
{
    [PermissionChecker(1005)]
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _permissionService.DeleteRole(Role);

            return RedirectToPage("Index");
        }
    }
}