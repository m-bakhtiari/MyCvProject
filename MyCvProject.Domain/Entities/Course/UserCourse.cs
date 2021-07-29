using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Course
{
    public class UserCourse
    {
        #region Constructor

        public UserCourse()
        {
            
        }
        #endregion

        /// <summary>
        /// آیدی دوره کاربر
        /// </summary>
        [Key]
        public int UC_Id { get; set; }

        /// <summary>
        /// آیدی کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// آیدی دوره
        /// </summary>
        public int CourseId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public User.User User { get; set; } 
        #endregion
    }
}
