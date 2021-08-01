using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;
using MyCvProject.Domain.ViewModels.User;

namespace MyCvProject.UI.Pages.Admin
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public AdminStatisticsViewModel AdminStatisticsViewModel { get; set; }
        public async Task OnGet()
        {
            AdminStatisticsViewModel = await _userService.GetAdminHomePageStatistics();
        }
    }
}
