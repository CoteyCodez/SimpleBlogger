using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleBlogger.Models.ViewModels
{
    public class PostWithTagAndCommentsViewModel
    {
        #region Post
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostBody { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        #endregion

        public Tag AssignedTag { get; set; }

        #region Comments 
        public Comment SubmittedComment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        #endregion



    }
}
