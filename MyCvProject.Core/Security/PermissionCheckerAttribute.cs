using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyCvProject.Core.Interfaces;

namespace MyCvProject.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private readonly int[] _permissionId;
        public PermissionCheckerAttribute(int[] permissionId)
        {
            _permissionId = permissionId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;
                foreach (var id in _permissionId)
                {
                    if (_permissionService.CheckPermission(id, userName).Result)
                    {
                        return;
                    }
                }
                context.Result = new RedirectResult("/Login?" + context.HttpContext.Request.Path);
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
