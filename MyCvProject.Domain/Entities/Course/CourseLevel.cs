using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Course
{
    public class CourseLevel
    {
        #region Constructor

        public CourseLevel()
        {

        }

        #endregion

        /// <summary>
        /// آیدی سطح دوره
        /// </summary>
        [Key]
        public int LevelId { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string LevelTitle { get; set; }

        #region Relations
        public List<Course> Courses { get; set; }

        #endregion  
    }
}
