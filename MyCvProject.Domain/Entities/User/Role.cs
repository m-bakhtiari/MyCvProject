using MyCvProject.Domain.Entities.Permissions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.User
{
    /// <summary>
    /// نقش کاربران
    /// </summary>
    public class Role
    {
        public Role()
        {
            
        }

        /// <summary>
        /// آیدی نقش
        /// </summary>
        [Key]
        public int RoleId { get; set; }

        /// <summary>
        /// عنوان نقش
        /// </summary>
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }

        /// <summary>
        /// آیا حذف شده است
        /// </summary>
        public bool IsDelete { get; set; }


        #region Relations
        /// <summary>
        /// نفش کاربران
        /// </summary>
        public virtual List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// سطح دسترسی هر نقش
        /// </summary>
        public List<RolePermission> RolePermissions { get; set; }


        #endregion
    }
}
