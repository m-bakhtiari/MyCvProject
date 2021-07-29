using System;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Course
{
    public class CourseEpisode
    {
        #region Constructor
        public CourseEpisode()
        {

        } 
        #endregion

        /// <summary>
        /// آیدی بخش
        /// </summary>
        [Key]
        public int EpisodeId { get; set; }

        /// <summary>
        /// آیدی دوره
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// عنوان بخش
        /// </summary>
        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EpisodeTitle { get; set; }

        /// <summary>
        /// زمان
        /// </summary>
        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }

        /// <summary>
        /// فایل
        /// </summary>
        [Display(Name = "فایل")]
        public string EpisodeFileName { get; set; }

        /// <summary>
        /// رایگان
        /// </summary>
        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }


        #region Relations
        public Course Course { get; set; }

        #endregion
    }
}
