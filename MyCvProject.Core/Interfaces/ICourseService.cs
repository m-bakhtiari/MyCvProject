using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCvProject.Core.ViewModels.Course;
using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Core.Interfaces
{
    public interface ICourseService
    {
        #region Group

        Task<List<CourseGroup>> GetAllGroup();
        Task<List<SelectListItem>> GetGroupForManageCourse();
        Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId);
        Task<List<SelectListItem>> GetTeachers();
        Task<List<SelectListItem>> GetLevels();
        Task<List<SelectListItem>> GetStatues();
        Task<CourseGroup> GetById(int groupId);
        Task AddGroup(CourseGroup group);
        Task UpdateGroup(CourseGroup group);

        #endregion

        #region Course

        Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin();

        Task<int> AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        Task<Course> GetCourseById(int courseId);
        Task UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);

        Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        Task<Course> GetCourseForShow(int courseId);

        Task<List<ShowCourseListItemViewModel>> GetPopularCourse();

        #endregion

        #region Episode

        Task<List<CourseEpisode>> GetListEpisodeCorse(int courseId);
        bool CheckExistFile(string fileName);
        Task<int> AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        Task<CourseEpisode> GetEpisodeById(int episodeId);
        Task EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        #endregion

        #region Comments

        Task AddComment(CourseComment comment);
        Task<Tuple<List<CourseComment>, int>> GetCourseComment(int courseId, int pageId = 1);

        #endregion
    }
}
