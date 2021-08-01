using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCvProject.Domain.ViewModels.User
{
    public class AdminStatisticsViewModel
    {
        public int UserCount { get; set; }
        public int CourseCount { get; set; }
        public int CourseGroupCount { get; set; }
        public int CommentCount { get; set; }
    }
}
