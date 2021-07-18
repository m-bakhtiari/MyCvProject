using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.ViewModels;
using MyCvProject.Core.Security;
using MyCvProject.Core.Services.Interfaces;
using MyCvProject.Domain.Entities.User;

namespace MyCvProject.Web.Pages.Admin.Roles
{
    [PermissionChecker(1002)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> RolesList { get; set; }

       
        public void OnGet()
        {
            RolesList = _permissionService.GetRoles();
        }

       
    }
}