using System;
using System.Collections.Generic;
using System.Text;

namespace MyCvProject.Domain.ViewModels.Course
{
    public class ShowCourseForAdminViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int EpisodeCount { get; set; }

    }
}
