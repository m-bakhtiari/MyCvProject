using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels;

namespace MyCvProject.UI.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId=1,string filterUserName="",string filterEmail="")
        {
            UserForAdminViewModel = _userService.GetUsers(pageId,filterEmail,filterUserName);
        }

       
    }
}