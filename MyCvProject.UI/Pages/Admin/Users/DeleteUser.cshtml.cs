using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Users
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin, Const.PermissionIdForDeleteUser, Const.PermissionIdForManageUser })]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserViewModel InformationUserViewModel { get; set; }
        public async void OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = await _userService.GetUserInformation(id);
        }

        public async Task<IActionResult> OnPost(int UserId)
        {
            await _userService.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}