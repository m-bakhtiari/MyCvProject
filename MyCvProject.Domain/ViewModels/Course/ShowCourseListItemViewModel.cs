using System;

namespace MyCvProject.Core.ViewModels.Course
{
    public class ShowCourseListItemViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
