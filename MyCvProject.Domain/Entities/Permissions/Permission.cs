using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCvProject.Domain.Entities.Permissions
{
    /// <summary>
    /// سطح دسترسی کاربران
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// آیدی نقش
        /// </summary>
        [Key]
        public int PermissionId { get; set; }

        /// <summary>
        /// عنوان نقش
        /// </summary>
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PermissionTitle { get; set; }

        /// <summary>
        /// آیدی والد نقش
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// لیست نقش های زیر مجموعه
        /// </summary>
        [ForeignKey(nameof(ParentID))]
        public List<Permission> Permissions { get; set; }

        /// <summary>
        /// لیست ارتباط با سطح دسترسی ها
        /// </summary>
        public List<RolePermission> RolePermissions { get; set; }


    }
}
