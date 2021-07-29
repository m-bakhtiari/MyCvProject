using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.User
{
    public class UserRole
    {
        #region Constructor
        public UserRole()
        {

        } 
        #endregion

        /// <summary>
        /// آیدی نقش هر کاربر
        /// </summary>
        [Key]
        public int UR_Id { get; set; }

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// آیدی نقش
        /// </summary>
        public int RoleId { get; set; }

        #region Relations

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion

    }
}
