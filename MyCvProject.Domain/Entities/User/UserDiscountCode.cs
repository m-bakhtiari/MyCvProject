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

        [Key]
        public int UD_Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }

        #region Relations
        public User User { get; set; }
        public Discount Discount { get; set; } 
        #endregion
    }
}
