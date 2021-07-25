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

        [Key]
        public int UC_Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public User.User User { get; set; } 
        #endregion
    }
}
