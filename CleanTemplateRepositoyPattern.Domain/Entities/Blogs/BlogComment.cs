using CleanTemplateRepositoyPattern.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Domain.Entities.Blogs
{
    public class BlogComment: BaseEntity
    {
        public Guid UserId { get; set; }
        public string CommentText { get; set; }     
        public bool IsApproved { get; set; }
        public Guid BlogPostId { get; set; }

        //Navigation

        public BlogPost blogPost { get; set; }


    }
}
