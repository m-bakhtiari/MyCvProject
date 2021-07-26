using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCvProject.Core.Services;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Infra.Data.Context;
using MyCvProject.Infra.Data.Repositories;
using System.Threading.Tasks;

namespace MyCvProject.Tests
{
    [TestClass]
    public class CourseGroupTest
    {
        [TestMethod]
        public async Task AddCourseGroupSuccessful()
        {
            var moqSet = new Mock<DbSet<CourseGroup>>();
            var moqContext = new Mock<MyCvProjectContext>();
            moqContext.Setup(g => g.CourseGroups).Returns(moqSet.Object);
            var group = new CourseGroup()
            {
                GroupTitle = "Asp.net core",
            };
            var courseRepository = new CourseRepository(moqContext.Object);
            var userRepository = new UserRepository(moqContext.Object);
            var userService = new UserService(userRepository);
            var courseService = new CourseService(courseRepository, userService);

            var groups = await courseService.GetAllGroup();
            var res = await courseService.AddGroup(group);
            Assert.AreEqual(true, res.IsSuccess);
            var newGroups = await courseService.GetAllGroup();
            Assert.AreNotEqual(groups.Count, newGroups.Count);
        }
    }
}
