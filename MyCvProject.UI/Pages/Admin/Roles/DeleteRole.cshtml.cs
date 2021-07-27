using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;
using MyCvProject.Domain.Entities.User;

namespace MyCvProject.UI.Pages.Admin.Roles
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin, Const.PermissionIdForDeleteRole, Const.PermissionIdForManageRole })]
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public async Task OnGet(int id)
        {
            Role = await _permissionService.GetRoleById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _permissionService.DeleteRole(Role);

            return RedirectToPage("Index");
        }
    }
}