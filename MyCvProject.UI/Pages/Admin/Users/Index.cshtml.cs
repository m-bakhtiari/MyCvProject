using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin.Users
{
    [PermissionChecker(new int[] { Const.PermissionIdForManageUser, Const.PermissionIdForAdmin })]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }

        public async Task OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = await _userService.GetUsers(pageId, filterEmail, filterUserName);
        }
    }
}