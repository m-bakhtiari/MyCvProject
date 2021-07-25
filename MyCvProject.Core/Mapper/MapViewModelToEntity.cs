using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.Entities.User;
using MyCvProject.Domain.ViewModels.Course;
using MyCvProject.Domain.ViewModels.User;

namespace MyCvProject.Core.Mapper
{
    public static class MapViewModelToEntity
    {
        #region User

        public static User ToUser(this UserViewModel userViewModel)
        {
            return new User()
            {
                UserId = userViewModel.UserId,
                Email = userViewModel.Email,
                UserName = userViewModel.Username,
                Password = userViewModel.Password
            };
        }

        #endregion

        #region Permission
        public static Role ToRole(this RoleViewModel roleViewModel)
        {
            return new Role()
            {
                RoleId = roleViewModel.RoleId,
                RoleTitle = roleViewModel.RoleTitle
            };
        }
        #endregion

        #region Course
        public static Course ToCourse(this CourseViewModel courseViewModel)
        {
            return new Course()
            {
                CourseId = courseViewModel.CourseId,
                GroupId = courseViewModel.GroupId,
                SubGroup = courseViewModel.SubGroup,
                TeacherId = courseViewModel.TeacherId,
                StatusId = courseViewModel.StatusId,
                LevelId = courseViewModel.StatusId,
                CourseTitle = courseViewModel.CourseTitle,
                CourseDescription = courseViewModel.CourseDescription,
                CoursePrice = courseViewModel.CoursePrice,
                Tags = courseViewModel.Tags,
                CourseImageName = courseViewModel.CourseImageName,
                DemoFileName = courseViewModel.DemoFileName
            };
        }
        public static CourseGroup ToCourseGroup(this CourseGroupViewModel courseGroupViewModel)
        {
            return new CourseGroup()
            {
                GroupId = courseGroupViewModel.GroupId,
                GroupTitle = courseGroupViewModel.GroupTitle,
                ParentId = courseGroupViewModel.ParentId
            };
        }
        public static CourseEpisode ToCourseEpisode(this CourseEpisodeViewModel courseEpisodeViewModel)
        {
            return new CourseEpisode()
            {
                CourseId = courseEpisodeViewModel.CourseId,
                IsFree = courseEpisodeViewModel.IsFree,
                EpisodeTitle = courseEpisodeViewModel.EpisodeTitle,
                EpisodeTime = courseEpisodeViewModel.EpisodeTime,
                EpisodeFileName = courseEpisodeViewModel.EpisodeFileName,
                EpisodeId = courseEpisodeViewModel.EpisodeId
            };
        }
        public static CourseComment ToCourseComment(this CourseCommentViewModel courseCommentViewModel)
        {
            return new CourseComment()
            {
                CourseId = courseCommentViewModel.CourseId,
                Comment = courseCommentViewModel.Comment,
                CommentId = courseCommentViewModel.CommentId
            };
        } 
        #endregion
    }
}
