using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos
{
    public class RequestBlugPostDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool? AllowComments { get; set; }
        public string Tags { get; set; }
        public string? MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string? PostImage { get; set; }
    }
}
