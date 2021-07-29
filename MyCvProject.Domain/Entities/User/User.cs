using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.User
{
    public class User
    {
        #region Constructor
        public User()
        {

        } 
        #endregion

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        /// <summary>
        /// کلمه عبور
        /// </summary>
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        /// <summary>
        /// کد فعال سازی
        /// </summary>
        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActiveCode { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        /// <summary>
        /// آواتار
        /// </summary>
        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserAvatar { get; set; }

        /// <summary>
        /// تاریخ ثبت نام
        /// </summary>
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// پاک شده است
        /// </summary>
        public bool IsDelete { get; set; }

        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<Wallet.Wallet> Wallets { get; set; }
        public virtual List<Course.Course> Courses { get; set; }
        public virtual List<Order.Order> Orders { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        #endregion
    }
}
