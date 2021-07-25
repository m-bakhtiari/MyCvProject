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
using System.Threading.Tasks;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Domain.ViewModels;

namespace MyCvProject.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseGroup>> GetAllGroup()
        {
            return await _courseRepository.GetAllGroup();
        }

        public async Task<List<SelectListItem>> GetGroupForManageCourse()
        {
            return await _courseRepository.GetGroupForManageCourse();
        }

        public async Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId)
        {
            return await _courseRepository.GetSubGroupForManageCourse(groupId);
        }

        public async Task<List<SelectListItem>> GetTeachers()
        {
            return await _courseRepository.GetTeachers();
        }

        public async Task<List<SelectListItem>> GetLevels()
        {
            return await _courseRepository.GetLevels();
        }

        public async Task<List<SelectListItem>> GetStatues()
        {
            return await _courseRepository.GetStatues();
        }

        public async Task<CourseGroup> GetById(int groupId)
        {
            return await _courseRepository.GetById(groupId);
        }

        public async Task AddGroup(CourseGroup group)
        {
            await _courseRepository.AddGroup(group);
        }

        public async Task UpdateGroup(CourseGroup group)
        {
            await _courseRepository.UpdateGroup(group);
        }

        public async Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin()
        {
            return await _courseRepository.GetCoursesForAdmin();
        }

        public async Task<int> AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";
            if (imgCourse != null && imgCourse.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image", course.CourseImageName);

                await using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgCourse.CopyToAsync(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb", course.CourseImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 250);
            }

            if (courseDemo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                var demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes", course.DemoFileName);
                await using var stream = new FileStream(demoPath, FileMode.Create);
                await courseDemo.CopyToAsync(stream);
            }

            return await _courseRepository.AddCourse(course);
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _courseRepository.GetCourseById(courseId);
        }

        public async Task UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
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

                await using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imgCourse.CopyToAsync(stream);
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
                await using var stream = new FileStream(demoPath, FileMode.Create);
                await courseDemo.CopyToAsync(stream);
            }
            await _courseRepository.UpdateCourse(course);
        }

        public async Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {

            return await _courseRepository.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups,
                 take);
        }

        public async Task<Course> GetCourseForShow(int courseId)
        {
            return await _courseRepository.GetCourseForShow(courseId);
        }

        public async Task<List<ShowCourseListItemViewModel>> GetPopularCourse()
        {
            return await _courseRepository.GetPopularCourse();
        }

        public async Task<List<CourseEpisode>> GetListEpisodeCorse(int courseId)
        {
            return await _courseRepository.GetListEpisodeCorse(courseId);
        }

        public bool CheckExistFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", fileName);
            return File.Exists(path);
        }

        public async Task<int> AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await episodeFile.CopyToAsync(stream);
            }

            return await _courseRepository.AddEpisode(episode);
        }

        public async Task<CourseEpisode> GetEpisodeById(int episodeId)
        {
            return await _courseRepository.GetEpisodeById(episodeId);
        }

        public async Task EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                File.Delete(deleteFilePath);

                episode.EpisodeFileName = episodeFile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await episodeFile.CopyToAsync(stream);
            }

            await _courseRepository.EditEpisode(episode);
        }

        public async Task AddComment(CourseComment comment)
        {
            await _courseRepository.AddComment(comment);
        }

        public async Task<Tuple<List<CourseComment>, int>> GetCourseComment(int courseId, int pageId = 1)
        {
            return await _courseRepository.GetCourseComment(courseId, pageId);
        }

        public async Task DeleteCourse(int courseId)
        {
            var course = await _courseRepository.GetCourseById(courseId);
            if (course == null) return;
            course.IsDelete = true;
            await _courseRepository.UpdateCourse(course);
        }

        public async Task<OpRes<int>> AddCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseTitle))
                return OpRes<int>.BuildError("عنوان دوره را وارد نمایید");
            course.CreateDate = DateTime.Now;
            var courseId = await _courseRepository.AddCourse(course);
            return OpRes<int>.BuildSuccess(courseId);
        }

        public async Task<OpRes> UpdateCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseTitle))
                return OpRes<int>.BuildError("عنوان دوره را وارد نمایید");
            course.UpdateDate = DateTime.Now;
            await _courseRepository.UpdateCourse(course);
            return OpRes.BuildSuccess();
        }
    }
}
