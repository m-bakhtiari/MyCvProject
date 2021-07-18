using MyCvProject.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Permissions
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }

    }
}
