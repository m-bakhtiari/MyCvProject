using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCvProject.Core.ViewModels.Course;
using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCvProject.Domain.ViewModels;

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
        Task<CourseGroup> GetCourseGroupById(int groupId);
        Task<OpRes> AddGroup(CourseGroup group);
        Task<OpRes> UpdateGroup(CourseGroup group);
        Task DeleteGroup(int groupId);

        #endregion

        #region Course

        Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin();

        Task<int> AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        Task<OpRes<int>> AddCourse(Course course);
        Task<OpRes> UpdateCourse(Course course);
        Task<Course> GetCourseById(int courseId);
        Task UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);

        Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = "", string getType = "all",
            string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        Task<Course> GetCourseForShow(int courseId);

        Task<List<ShowCourseListItemViewModel>> GetPopularCourse();

        Task DeleteCourse(int courseId);

        #endregion

        #region Episode

        Task<List<CourseEpisode>> GetListEpisodeCorse(int courseId);
        bool CheckExistFile(string fileName);
        Task<int> AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        Task<OpRes<int>> AddEpisode(CourseEpisode episode);
        Task<CourseEpisode> GetEpisodeById(int episodeId);
        Task EditEpisode(CourseEpisode episode, IFormFile episodeFile);
        Task<OpRes> EditEpisode(CourseEpisode episode);
        Task<List<CourseEpisode>> GetAllEpisode();
        Task DeleteEpisode(int episodeId);
        #endregion

        #region Comments

        Task<OpRes> AddComment(CourseComment comment, string username);
        Task<Tuple<List<CourseComment>, int>> GetCourseComment(int courseId, int pageId = 1);
        Task<List<CourseComment>> GetCourseComment(int courseId);

        #endregion
    }
}
