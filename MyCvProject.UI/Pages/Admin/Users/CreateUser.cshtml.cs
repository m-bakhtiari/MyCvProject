using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Users
{
    [PermissionChecker(new int[] { Const.PermissionIdForAdmin, Const.PermissionIdForCreateUser, Const.PermissionIdForManageUser })]
    public class CreateUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }


        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }

        public async Task OnGet()
        {
            ViewData["Roles"] = await _permissionService.GetRoles();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = await _userService.AddUserFromAdmin(CreateUserViewModel);

            //Add Roles
            await _permissionService.AddRolesToUser(SelectedRoles, userId);

            return Redirect("/Admin/Users");

        }
    }
}