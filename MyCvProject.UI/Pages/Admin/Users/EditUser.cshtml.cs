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
    [PermissionChecker(new []{Const.PermissionIdForAdmin, Const.PermissionIdForManageUser , Const.PermissionIdForEditUser })]
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }
        public async Task OnGet(int id)
        {
            EditUserViewModel = await _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = await _permissionService.GetRoles();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.EditUserFromAdmin(EditUserViewModel);

            //Edit Roles
            await _permissionService.EditRolesUser(EditUserViewModel.UserId, SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}