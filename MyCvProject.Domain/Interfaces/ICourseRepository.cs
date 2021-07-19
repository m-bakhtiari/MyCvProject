using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCvProject.Core.ViewModels.Course;
using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;

namespace MyCvProject.Domain.Interfaces
{
    public interface ICourseRepository
    {
        List<CourseGroup> GetAllGroup();

        List<SelectListItem> GetGroupForManageCourse();

        List<SelectListItem> GetSubGroupForManageCourse(int groupId);

        List<SelectListItem> GetTeachers();

        List<SelectListItem> GetLevels();

        List<SelectListItem> GetStatues();

        CourseGroup GetById(int groupId);

        void AddGroup(CourseGroup @group);

        void UpdateGroup(CourseGroup @group);
        List<ShowCourseForAdminViewModel> GetCoursesForAdmin();
        int AddCourse(Course course);
        Course GetCourseById(int courseId);
        void UpdateCourse(Course course);

        Tuple<List<ShowCourseListItemViewModel>, int> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);

        Course GetCourseForShow(int courseId);

        List<ShowCourseListItemViewModel> GetPopularCourse();


        List<CourseEpisode> GetListEpisodeCorse(int courseId);


        int AddEpisode(CourseEpisode episode);


        CourseEpisode GetEpisodeById(int episodeId);


        void EditEpisode(CourseEpisode episode);


        void AddComment(CourseComment comment);

        Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId = 1);

    }
}
