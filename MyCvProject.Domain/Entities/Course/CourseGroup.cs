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

        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }
        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

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
