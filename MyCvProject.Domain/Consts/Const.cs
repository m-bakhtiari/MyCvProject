using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCvProject.Domain.Consts
{
   public class Const
   {
       public const int RoleIdForUser = 1001;
       public const int RoleIdForAdmin = 1002;
       public const int RoleIdForTeacher = 1003;

       public const int PermissionIdForAdmin = 1001;
       public const int PermissionIdForManageUser = 1002;
       public const int PermissionIdForCreateUser = 1003;
       public const int PermissionIdForEditUser = 1004;
       public const int PermissionIdForDeleteUser = 1005;
       public const int PermissionIdForManageRole = 1006;
       public const int PermissionIdForCreateRole = 1007;
       public const int PermissionIdForEditRole = 1008;
       public const int PermissionIdForDeleteRole = 1009;

       public const int WalletTypeIdForIncrease = 1;
       public const int WalletTypeIdForDecrease = 2;

       public const string DefaultUserAvatar = "Defult.jpg";

       public const string SiteUrl = "https://localhost:44380";
       public const string VerifyCodeJwt = "this is my custom Secret key for authnetication";
    }
}
