using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.ViewModels;

namespace MyCvProject.UI.Pages.Admin.Users
{
    public class ListDeleteUsersModel : PageModel
    {
        private readonly IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userService.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

    }
}