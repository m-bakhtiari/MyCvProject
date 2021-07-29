using System;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Wallet
{
    public class Wallet
    {
        #region Constructor
        public Wallet()
        {

        } 
        #endregion

        /// <summary>
        /// آیدی کیف پول
        /// </summary>
        [Key]
        public int WalletId { get; set; }

        /// <summary>
        /// نوع تراکنش
        /// </summary>
        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }

        /// <summary>
        /// مبلغ
        /// </summary>
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        /// <summary>
        /// شرح
        /// </summary>
        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        /// <summary>
        /// تایید شده
        /// </summary>
        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }

        /// <summary>
        /// تاریخ و ساعت
        /// </summary>
        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }

        #region Relations
        public virtual User.User User { get; set; }
        public virtual WalletType WalletType { get; set; } 
        #endregion

    }
}
