namespace MyCvProject.Domain.ViewModels.Course
{
    public class CourseGroupViewModel
    {
        /// <summary>
        /// آیدی گروه
        /// فقط برای حالت ویرایش پر شود
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// عنوان گروه
        /// </summary>
        public string GroupTitle { get; set; }

        /// <summary>
        /// آیدی گروه
        /// برای گروه اصلی پر نشود
        /// برای زیر گروه آیدی گروه اصلی آن پر شود
        /// </summary>
        public int? ParentId { get; set; }
    }
}
