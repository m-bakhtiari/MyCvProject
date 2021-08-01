namespace MyCvProject.Domain.ViewModels.Course
{
    public class CourseViewModel
    {
        /// <summary>
        /// آیدی دوره
        /// فقط برای حالت ویرایش پر شود
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// آیدی گروه
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// آیدی زیر گروه
        /// </summary>
        public int? SubGroup { get; set; }

        /// <summary>
        /// آیدی مدرس دوره
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// آیدی وضعیت دوره
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// آیدی سطح دوره
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// عنوان دوره
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// توضیحات دوره
        /// </summary>
        public string CourseDescription { get; set; }

        /// <summary>
        /// قیمت دوره
        /// </summary>
        public int CoursePrice { get; set; }

        /// <summary>
        /// کلمات کلیدی
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// عکس دوره
        /// </summary>
        public string CourseImageName { get; set; }

        /// <summary>
        /// نام فایل پیش نمایش و دمو دوره
        /// </summary>
        public string DemoFileName { get; set; }
    }
}
