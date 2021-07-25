namespace MyCvProject.Domain.ViewModels.Course
{
    public class CourseCommentViewModel
    {
        /// <summary>
        /// آیدی کامنت
        /// فقط برای ویرایش کامنت خاص پر شود
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// آیدی دوره
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// متن کامنت
        /// </summary>
        public string Comment { get; set; }
    }
}
