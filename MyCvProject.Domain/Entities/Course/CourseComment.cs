﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyCvProject.Domain.Entities.Course
{
    public class CourseComment
    {
        #region Constructor

        public CourseComment()
        {
            
        }
        #endregion

        [Key]
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }

        [MaxLength(700)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }

        #region Relations
        public Course Course { get; set; }
        public User.User User { get; set; } 
        #endregion
    }
}
