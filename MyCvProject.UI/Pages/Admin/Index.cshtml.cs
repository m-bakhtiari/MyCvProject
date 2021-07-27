using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Consts;

namespace MyCvProject.UI.Pages.Admin
{
    [PermissionChecker(new[] { Const.PermissionIdForAdmin })]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
