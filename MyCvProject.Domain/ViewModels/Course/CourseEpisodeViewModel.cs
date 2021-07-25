using System;

namespace MyCvProject.Domain.ViewModels.Course
{
    public class CourseEpisodeViewModel
    {
        /// <summary>
        /// آیدی بخش دوره
        /// فقط در حالت ویرایش پر شود
        /// </summary>
        public int EpisodeId { get; set; }

        /// <summary>
        /// آیدی دوره
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// عنوان بخش دوره
        /// </summary>
        public string EpisodeTitle { get; set; }

        /// <summary>
        /// مدت دوره
        /// </summary>
        public TimeSpan EpisodeTime { get; set; }

        /// <summary>
        /// نام فایل دوره
        /// </summary>
        public string EpisodeFileName { get; set; }

        /// <summary>
        /// رایگان است یا خیر
        /// </summary>
        public bool IsFree { get; set; }
    }
}
