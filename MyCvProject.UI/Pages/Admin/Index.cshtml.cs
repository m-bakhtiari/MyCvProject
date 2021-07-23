using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin
{
    [PermissionChecker(Const.PermissionIdForAdmin)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
