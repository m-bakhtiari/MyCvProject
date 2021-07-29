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

        /// <summary>
        /// آیدی وضعین دوره
        /// </summary>
        [Key]
        public int StatusId { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }

        #region Relations
        public List<Course> Courses { get; set; }

        #endregion
    }
}
