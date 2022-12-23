using AutoMapper;
using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos;
using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogPost, RequestBlugPostDTO>().ReverseMap();
        }
      
       
    }
}
