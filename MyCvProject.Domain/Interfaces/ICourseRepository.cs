using Microsoft.AspNetCore.Mvc.Rendering;
using MyCvProject.Core.ViewModels.Course;
using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCvProject.Domain.Interfaces
{
    public interface ICourseRepository
    {
        #region Course Group

        /// <summary>
        /// گرفتن تمام دسته بندی ها
        /// </summary>
        /// <returns></returns>
        Task<List<CourseGroup>> GetAllGroup();

        /// <summary>
        /// گرفتن گروه های اصلی بدون زیر گروه
        /// </summary>
        /// <returns></returns>
        Task<List<SelectListItem>> GetGroupForManageCourse();

        /// <summary>
        /// گرفتن زیر گروه ها بر اساس آیدی گروه اصلی
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId);

        /// <summary>
        /// گرفتن گروه با آیدی
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<CourseGroup> GetCourseGroupById(int groupId);

        /// <summary>
        /// افزودن گروه جدید
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task AddGroup(CourseGroup @group);

        /// <summary>
        /// ویرایش گروه
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task UpdateGroup(CourseGroup @group);

        #endregion

        #region Course

        /// <summary>
        /// گرفتن دوره ها برای صفحه ادمین
        /// </summary>
        /// <returns></returns>
        Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin();

        /// <summary>
        /// افزودن دوره جدید
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        Task<int> AddCourse(Course course);

        /// <summary>
        /// گرفتن دوره با آیدی
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<Course> GetCourseById(int courseId);

        /// <summary>
        /// ویرایش دوره
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        Task UpdateCourse(Course course);

        /// <summary>
        /// گرفتن دوره ها با فیلتر و صفحه بندی
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="filter"></param>
        /// <param name="getType"></param>
        /// <param name="orderByType"></param>
        /// <param name="startPrice"></param>
        /// <param name="endPrice"></param>
        /// <param name="selectedGroups"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        /// <summary>
        /// گرفتن دوره با جداول مرتبط 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<Course> GetCourseForShow(int courseId);

        /// <summary>
        /// گرفتن دوره های محبوب
        /// </summary>
        /// <returns></returns>
        Task<List<ShowCourseListItemViewModel>> GetPopularCourse();

        #endregion

        #region Episode

        /// <summary>
        /// گرفتن لیست بخش های هر دوره
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<List<CourseEpisode>> GetListEpisodeCorse(int courseId);

        /// <summary>
        /// افزودن بخش جدید
        /// </summary>
        /// <param name="episode"></param>
        /// <returns></returns>
        Task<int> AddEpisode(CourseEpisode episode);

        /// <summary>
        /// گرفتن بخش با آیدی
        /// </summary>
        /// <param name="episodeId"></param>
        /// <returns></returns>
        Task<CourseEpisode> GetEpisodeById(int episodeId);

        /// <summary>
        /// ویرایش بخش
        /// </summary>
        /// <param name="episode"></param>
        /// <returns></returns>
        Task EditEpisode(CourseEpisode episode);

        /// <summary>
        /// گرفتن تمام بخش ها
        /// </summary>
        /// <returns></returns>
        Task<List<CourseEpisode>> GetAllEpisode();

        /// <summary>
        /// حذف بخش
        /// </summary>
        /// <param name="episodeId"></param>
        /// <returns></returns>
        Task DeleteEpisode(int episodeId);

        #endregion

        #region Course Levels

        /// <summary>
        /// گرفتن سطح های دوره
        /// </summary>
        /// <returns></returns>
        Task<List<SelectListItem>> GetLevels();

        #endregion

        #region Course Status

        /// <summary>
        /// گرفتن وضعیت های دوره
        /// </summary>
        /// <returns></returns>
        Task<List<SelectListItem>> GetStatues();
        #endregion

        #region Course Comment

        /// <summary>
        /// افزودن کامنت جدید
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task AddComment(CourseComment comment);

        /// <summary>
        /// گرفتن کامنت های یک دوره با صفحه بندی
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        Task<Tuple<List<CourseComment>, int>> GetCourseComment(int courseId, int pageId = 1);

        /// <summary>
        /// گرفتن تمام کامنت های یک دوره 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<List<CourseComment>> GetCourseComment(int courseId);
        #endregion

        #region Teacher
        /// <summary>
        /// گرفتن لیست کاربرانی که نقش استاد دارند
        /// </summary>
        /// <returns></returns>
        Task<List<SelectListItem>> GetTeachers();
        #endregion
    }
}
