using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleBlogger.Models
{
    public class PostTags
    {
        //public int Id { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Posts Post { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tags Tag { get; set; }
    }
}
