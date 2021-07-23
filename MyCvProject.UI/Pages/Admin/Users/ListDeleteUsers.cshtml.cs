using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Users
{
    [PermissionChecker(Const.PermissionIdForAdmin)]
    [PermissionChecker(Const.PermissionIdForManageUser)]
    public class ListDeleteUsersModel : PageModel
    {
        private readonly IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }

        public async Task OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = await _userService.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

    }
}