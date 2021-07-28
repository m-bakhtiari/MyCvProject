using Moq;
using MyCvProject.Core.Interfaces;
using MyCvProject.Domain.Entities.Course;
using MyCvProject.Domain.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCvProject.Infra.Data.Context;
using Xunit;
using MyCvProject.Api.Controllers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MyCvProject.Tests.ControllerTests
{
    [TestClass]
    public class CourseGroupControllerTests
    {
        private readonly Mock<ICourseService> _courseServiceMock = new Mock<ICourseService>();

        [TestMethod]
        public async Task AddCourseGroup_ShouldBeOk()
        {
            //Arrange
            var group = new CourseGroup() { GroupTitle = "Title From Test Project" };
            var mockSet = new Mock<DbSet<CourseGroup>>();
            var mockContext = new Mock<MyCvProjectContext>();
            mockContext.Setup(c => c.CourseGroups).Returns(mockSet.Object);

            //Act
            var groupService = new Mock<ICourseService>();
            groupService.Setup(g => g.AddGroup(group)).ReturnsAsync(new OpRes());
            var result = await CourseGroupController

            //Assert
            Assert.AreEqual(result.AddCourseGroup());
        }

    }
}
