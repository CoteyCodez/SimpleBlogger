using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleBlogger.Models.ViewModels
{
    public class PostWithTagsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UserId { get; set; }        
        public Tag? AssignedTag { get; set; }
    }
}
