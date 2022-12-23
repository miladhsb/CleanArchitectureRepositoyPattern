using CleanTemplateRepositoyPattern.Domain.Common;

using CleanTemplateRepositoyPattern.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Domain.Entities.Blogs
{
    public class BlogPost: BaseEntity
    {
       
        public string Title { get; set; }
        public string PostSlug { get; set; }
        public string Body { get; set; }
        public bool AllowComments { get; set; }
        public string Tags { get; set; }
        public string? MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }

        public string? PostImage { get; set; }
        public PostState postState { get; set; }


        //Navigation

        public ICollection<BlogComment> blogComments { get; set; }


    }
}
