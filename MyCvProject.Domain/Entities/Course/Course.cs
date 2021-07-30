using MyCvProject.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCvProject.Domain.Entities.Course
{
    public class Course
    {
        #region Constructor

        public Course()
        {

        }
        #endregion

        /// <summary>
        /// آیدی دوره
        /// </summary>
        [Key]
        public int CourseId { get; set; }

        /// <summary>
        /// آیدی گروه
        /// </summary>
        [Required]
        public int GroupId { get; set; }

        /// <summary>
        /// آیدی زیر گروه
        /// </summary>
        public int? SubGroup { get; set; }

        /// <summary>
        /// آیدی مدرس 
        /// </summary>
        [Required]
        public int TeacherId { get; set; }

        /// <summary>
        /// آیدی وضعیت دوره
        /// </summary>
        [Required]
        public int StatusId { get; set; }

        /// <summary>
        /// آیدی سطح دوره
        /// </summary>
        [Required]
        public int LevelId { get; set; }

        /// <summary>
        /// عنوان دوره
        /// </summary>
        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        /// <summary>
        /// شرح دوره
        /// </summary>
        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        /// <summary>
        /// قیمت دوره
        /// </summary>
        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        /// <summary>
        /// کلمات کلیدی
        /// </summary>
        [MaxLength(600)]
        public string Tags { get; set; }

        /// <summary>
        /// نام عکس
        /// </summary>
        [MaxLength(50)]
        public string CourseImageName { get; set; }

        /// <summary>
        /// نام فایل فیلم پیش نمایش دوره
        /// </summary>
        [MaxLength(100)]
        public string DemoFileName { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// تاریخ به روز رسانی
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// حذف شده
        /// </summary>
        public bool IsDelete { get; set; }

        #region Relations

        [ForeignKey(nameof(TeacherId))]
        public User.User User { get; set; }

        [ForeignKey(nameof(GroupId))]
        public CourseGroup CourseGroup { get; set; }

        [ForeignKey(nameof(SubGroup))]
        public CourseGroup Group { get; set; }

        [ForeignKey(nameof(StatusId))]
        public CourseStatus CourseStatus { get; set; }

        [ForeignKey(nameof(LevelId))]
        public CourseLevel CourseLevel { get; set; }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<CourseComment> CourseComments { get; set; }

        #endregion
    }
}
