﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Domain.Entities.User;
using System.Collections.Generic;

namespace MyCvProject.UI.Pages.Admin.Roles
{
    [PermissionChecker(1002)]
    public class IndexModel : PageModel
    {
        private readonly IPermissionService _permissionService;

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