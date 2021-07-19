using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCvProject.Core.Convertors;
using MyCvProject.Core.Generator;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Security;
using MyCvProject.Core.ViewModels.Course;
using MyCvProject.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyCvProject.Domain.Interfaces;

namespace MyCvProject.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<CourseGroup> GetAllGroup()
        {
            return _courseRepository.GetAllGroup();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _courseRepository.GetGroupForManageCourse();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _courseRepository.GetSubGroupForManageCourse(groupId);
        }

        public List<SelectListItem> GetTeachers()
        {
            return _courseRepository.GetTeachers();
        }

        public List<SelectListItem> GetLevels()
        {
            return _courseRepository.GetLevels();
        }

        public List<SelectListItem> GetStatues()
        {
            return _courseRepository.GetStatues();
        }

        public CourseGroup GetById(int groupId)
        {
            return _courseRepository.GetById(groupId);
        }

        public void AddGroup(CourseGroup group)
        {
            _courseRepository.AddGroup(group);
        }

        public void UpdateGroup(CourseGroup group)
        {
            _courseRepository.UpdateGroup(group);
        }

        public List<ShowCourseForAdminViewModel> GetCoursesForAdmin()
        {
            return _courseRepository.GetCoursesForAdmin();
        }

        public int AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";
            if (imgCourse != null && imgCourse.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 250);
            }

            if (courseDemo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                var demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFileName);
                using var stream = new FileStream(demoPath, FileMode.Create);
                courseDemo.CopyTo(stream);
            }

            return _courseRepository.AddCourse(course);
        }

        public Course GetCourseById(int courseId)
        {
            return _courseRepository.GetCourseById(courseId);
        }

        public void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (imgCourse != null && imgCourse.IsImage())
            {
                if (course.CourseImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.CourseImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.CourseImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 250);
            }

            if (courseDemo != null)
            {
                if (course.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFileName);
                using var stream = new FileStream(demoPath, FileMode.Create);
                courseDemo.CopyTo(stream);
            }
            _courseRepository.UpdateCourse(course);
        }

        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {

            return _courseRepository.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups,
                 take);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _courseRepository.GetCourseForShow(courseId);
        }

        public List<ShowCourseListItemViewModel> GetPopularCourse()
        {
            return _courseRepository.GetPopularCourse();
        }

        public List<CourseEpisode> GetListEpisodeCorse(int courseId)
        {
            return _courseRepository.GetListEpisodeCorse(courseId);
        }

        public bool CheckExistFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", fileName);
            return File.Exists(path);
        }

        public int AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            return _courseRepository.AddEpisode(episode);
        }

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _courseRepository.GetEpisodeById(episodeId);
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                File.Delete(deleteFilePath);

                episode.EpisodeFileName = episodeFile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _courseRepository.EditEpisode(episode);
        }

        public void AddComment(CourseComment comment)
        {
            _courseRepository.AddComment(comment);
        }

        public Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId = 1)
        {
            return _courseRepository.GetCourseComment(courseId, pageId);
        }
    }
}
