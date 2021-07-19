using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Security;

namespace MyCvProject.UI.Pages.Admin
{
    [PermissionChecker(1)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
