using MyCvProject.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Permissions
{
    public class RolePermission
    {
        #region Constructor

        public RolePermission()
        {
            
        }

        #endregion

        /// <summary>
        /// آیدی سطح دسترسی هر نقش
        /// </summary>
        [Key]
        public int RP_Id { get; set; }

        /// <summary>
        /// آیدی نقش
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// آیدی سطح دسترسی
        /// </summary>
        public int PermissionId { get; set; }

        #region Relations
        public Role Role { get; set; }
        public Permission Permission { get; set; } 
        #endregion
    }
}
