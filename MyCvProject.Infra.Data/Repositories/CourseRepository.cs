using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCvProject.Core.ViewModels.Course;
using MyCvProject.Domain.Consts;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCvProject.Infra.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MyCvProjectContext _context;

        public CourseRepository(MyCvProjectContext context)
        {
            _context = context;
        }

        public async Task<List<CourseGroup>> GetAllGroup()
        {
            return await _context.CourseGroups.Include(c => c.CourseGroups).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetGroupForManageCourse()
        {
            return await _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId)
        {
            return await _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetTeachers()
        {
            return await _context.UserRoles.Where(r => r.RoleId == Const.RoleIdForTeacher).Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.UserName
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetLevels()
        {
            return await _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToListAsync();

        }

        public async Task<List<SelectListItem>> GetStatues()
        {
            return await _context.CourseStatuses.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToListAsync();
        }

        public async Task<CourseGroup> GetCourseGroupById(int groupId)
        {
            return await _context.CourseGroups.FindAsync(groupId);
        }

        public async Task AddGroup(CourseGroup @group)
        {
            await _context.CourseGroups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroup(CourseGroup @group)
        {
            _context.CourseGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShowCourseForAdminViewModel>> GetCoursesForAdmin()
        {
            return await _context.Courses.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                EpisodeCount = c.CourseEpisodes.Count
            }).ToListAsync();
        }

        public async Task<int> AddCourse(Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();

            return course.CourseId;
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<ShowCourseListItemViewModel>, int>> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 8;

            IQueryable<Course> result = _context.Courses;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }
                case "free":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }

            }

            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "updatedate":
                    {
                        result = result.OrderByDescending(c => c.UpdateDate);
                        break;
                    }
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < startPrice);
            }


            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
                }

            }

            int skip = (pageId - 1) * take;

            int pageCount = await result.Include(c => c.CourseEpisodes).Include(c => c.CourseGroup)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    Title = c.CourseTitle,
                    CourseGroup = c.CourseGroup.GroupTitle,
                    TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks)),
                }).CountAsync() / take;

            var query = await result.Include(c => c.CourseEpisodes).Include(c => c.CourseGroup)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    Title = c.CourseTitle,
                    CourseGroup = c.CourseGroup.GroupTitle,
                    //TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                }).Skip(skip).Take(take).ToListAsync();


            return Tuple.Create(query, pageCount);
        }

        public async Task<Course> GetCourseForShow(int courseId)
        {
            return await _context.Courses.Include(c => c.CourseEpisodes)
                .Include(c => c.CourseStatus).Include(c => c.CourseLevel)
                .Include(c => c.User).Include(c => c.UserCourses)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        public async Task<List<ShowCourseListItemViewModel>> GetPopularCourse()
        {
            return await _context.Courses.Include(c => c.OrderDetails)
                .Where(c => c.OrderDetails.Any())
                .OrderByDescending(d => d.OrderDetails.Count)
                .Take(8)
                .Select(c => new ShowCourseListItemViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    Title = c.CourseTitle,
                    //TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                })
                .ToListAsync();
        }

        public async Task<List<CourseEpisode>> GetListEpisodeCorse(int courseId)
        {
            return await _context.CourseEpisodes.Where(e => e.CourseId == courseId).ToListAsync();
        }

        public async Task<int> AddEpisode(CourseEpisode episode)
        {
            await _context.CourseEpisodes.AddAsync(episode);
            await _context.SaveChangesAsync();
            return episode.EpisodeId;
        }

        public async Task<CourseEpisode> GetEpisodeById(int episodeId)
        {
            return await _context.CourseEpisodes.FindAsync(episodeId);
        }

        public async Task EditEpisode(CourseEpisode episode)
        {
            _context.CourseEpisodes.Update(episode);
            await _context.SaveChangesAsync();
        }

        public async Task AddComment(CourseComment comment)
        {
            await _context.CourseComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<CourseComment>, int>> GetCourseComment(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            int pageCount = await _context.CourseComments.Where(c => !c.IsDelete && c.CourseId == courseId).CountAsync() / take;

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            return Tuple.Create(
                await _context.CourseComments.Include(c => c.User).Where(c => !c.IsDelete && c.CourseId == courseId)
                    .Skip(skip).Take(take)
                    .OrderByDescending(c => c.CreateDate).ToListAsync(), pageCount);
        }

        public async Task<List<CourseComment>> GetCourseComment(int courseId)
        {
            return await _context.CourseComments.ToListAsync();
        }

        public async Task<List<CourseEpisode>> GetAllEpisode()
        {
            return await _context.CourseEpisodes.ToListAsync();
        }

        public async Task DeleteEpisode(int episodeId)
        {
            var episode = await _context.CourseEpisodes.FirstOrDefaultAsync(x => x.EpisodeId == episodeId);
            _context.CourseEpisodes.Remove(episode);
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> GetMainCourseGroupCount()
        {
            return await _context.CourseGroups.CountAsync(x => x.ParentId == null);
        }

        public async Task<int> GetCourseCount()
        {
            return await _context.Courses.CountAsync();
        }

        public async Task<int> GetCourseCommentCount()
        {
            return await _context.CourseComments.CountAsync();
        }
    }
}
