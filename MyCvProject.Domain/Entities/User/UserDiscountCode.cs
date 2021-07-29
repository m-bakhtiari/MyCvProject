using MyCvProject.Domain.Entities.Order;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.User
{
    public class UserDiscountCode
    {
        #region Constructor

        public UserDiscountCode()
        {
            
        }

        #endregion

        /// <summary>
        /// آیدی کد تخفیف هر کاربر
        /// </summary>
        [Key]
        public int UD_Id { get; set; }

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// آیدی کد تخفیف
        /// </summary>
        public int DiscountId { get; set; }

        #region Relations
        public User User { get; set; }
        public Discount Discount { get; set; } 
        #endregion
    }
}
