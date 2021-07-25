using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Course
{
    public class CourseStatus
    {
        #region Constructor

        public CourseStatus()
        {
            
        }
        #endregion

        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }

        #region Relations
        public List<Course> Courses { get; set; }

        #endregion
    }
}
