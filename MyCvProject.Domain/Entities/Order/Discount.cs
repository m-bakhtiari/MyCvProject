using MyCvProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Order
{
    public class Discount
    {
        #region Constructor

        public Discount()
        {

        }
        #endregion

        /// <summary>
        /// آیدی کد تخفیف
        /// </summary>
        [Key]
        public int DiscountId { get; set; }

        /// <summary>
        /// کد
        /// </summary>
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        /// <summary>
        /// درصد
        /// </summary>
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int DiscountPercent { get; set; }

        /// <summary>
        /// تعداد استفاده مجدد
        /// </summary>
        public int? UsableCount { get; set; }

        /// <summary>
        /// تاریخ شروع اعتبار
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// تاریخ پایان اعتبار
        /// </summary>
        public DateTime? EndDate { get; set; }

        #region Relations
        public List<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion   
    }
}
