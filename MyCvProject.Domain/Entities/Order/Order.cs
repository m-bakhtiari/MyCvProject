using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Order
{
    public class Order
    {
        #region Constructor

        public Order()
        {
            
        }
        #endregion

        /// <summary>
        /// آیدی پرداخت
        /// </summary>
        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// مجموع فاکتور
        /// </summary>
        [Required]
        public int OrderSum { get; set; }

        /// <summary>
        /// نهایی شده است
        /// </summary>
        public bool IsFinaly { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        #region Relations
        public virtual User.User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } 
        #endregion
    }
}
