using System;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Course
{
    public class CourseComment
    {
        #region Constructor

        public CourseComment()
        {
            
        }
        #endregion

        /// <summary>
        /// آیدی کامنت
        /// </summary>
        [Key]
        public int CommentId { get; set; }

        /// <summary>
        /// آیدی دوره
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// متن کامنت
        /// </summary>
        [MaxLength(700)]
        public string Comment { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// حذف شده
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// آیا ادمین خوانده است
        /// </summary>
        public bool IsAdminRead { get; set; }

        #region Relations
        public Course Course { get; set; }
        public User.User User { get; set; } 
        #endregion
    }
}
