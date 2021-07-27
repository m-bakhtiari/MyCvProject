using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Roles
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin, Const.PermissionIdForManageRole })]
    public class IndexModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> RolesList { get; set; }


        public async Task OnGet()
        {
            RolesList = await _permissionService.GetRoles();
        }
    }
}