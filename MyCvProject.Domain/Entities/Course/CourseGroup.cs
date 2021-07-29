using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCvProject.Domain.Entities.Course
{
    public class CourseGroup
    {
        #region Constructor

        public CourseGroup()
        {
            
        }
        #endregion

        /// <summary>
        /// آیدی گروه
        /// </summary>
        [Key]
        public int GroupId { get; set; }

        /// <summary>
        /// عنوان گروه
        /// </summary>
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }

        /// <summary>
        /// حذف شده ؟
        /// </summary>
        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// گروه اصلی
        /// </summary>
        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey(nameof(ParentId))]
        public List<CourseGroup> CourseGroups { get; set; }

        [NotMapped]
        [InverseProperty(nameof(Course.CourseGroup))]
        public List<Course> Courses { get; set; }

        [NotMapped]
        [InverseProperty(nameof(Course.Group))]
        public List<Course> SubGroup { get; set; } 
        #endregion
    }
}
