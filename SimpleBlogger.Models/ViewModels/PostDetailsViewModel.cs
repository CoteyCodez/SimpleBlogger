using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleBlogger.Models.ViewModels
{
    public class PostDetailsViewModel
    {
        #region Post
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostBody { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        #endregion

        #region Comments
        public Comment submittedComment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        #endregion

    }
}
