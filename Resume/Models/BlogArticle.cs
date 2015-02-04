using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Resume.Models
{
    public class BlogArticle
    {
        public int Id { get; set; }
       
        [DisplayFormat(DataFormatString= "{0:MM/dd/yyyy}", ApplyFormatInEditMode=true)]
        public DateTimeOffset CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset UpdateDate { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        [Required]
        public string Body { get; set; }
        public string MediaUrl { get; set; }
        public bool Published{ get; set;}
        public string Slug { get; set; }

        public virtual ICollection<BlogComment> Comments { get; set; }

        public BlogArticle()
        {
            Comments = new HashSet<BlogComment>();
        }
    }
    
}