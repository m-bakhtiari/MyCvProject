using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCvProject.Domain.Entities.Order
{
    public class OrderDetail
    {
        #region Constructor
        public OrderDetail()
        {

        } 
        #endregion

        /// <summary>
        /// آیدی جزییات پرداخت
        /// </summary>
        [Key]
        public int DetailId { get; set; }

        /// <summary>
        /// آیدی پرداخت
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// آیدی دوره
        /// </summary>
        [Required]
        public int CourseId { get; set; }

        /// <summary>
        /// تعداد
        /// </summary>
        [Required]
        public int Count { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Required]
        public int Price { get; set; }

        #region Relations
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Course.Course Course { get; set; } 
        #endregion

    }
}
