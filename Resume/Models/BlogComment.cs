using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int ArticleId {get; set;}
        public string AuthorId {get; set;}
        public string Body {get; set;}
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreateDate {get; set;}
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset UpdateDate {get; set;}
        public string UpdateReason {get; set;}

        public virtual BlogArticle Article { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}