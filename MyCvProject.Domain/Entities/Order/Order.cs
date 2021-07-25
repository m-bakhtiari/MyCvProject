﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Order
{
    public class Order
    {
        #region Constructor

        public Order()
        {
            
        }
        #endregion

        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        #region Relations
        public virtual User.User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; } 
        #endregion
    }
}
