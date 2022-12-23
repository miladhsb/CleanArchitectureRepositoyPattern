using AutoMapper;
using CleanArchitectureRepositoyPattern.Application.UnitTests.Mocks;
using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos;
using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos.Validator;
using CleanTemplateRepositoyPattern.Application.Profiles;
using CleanTemplateRepositoyPattern.Application.Services.BlogPostService;
using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using CleanTemplateRepositoyPattern.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureRepositoyPattern.Application.UnitTests.Services
{
    public class BlogPostServiceTest
    {



        [Fact]
        public async void CreatePostTest()
        {
            var requestBlugPostDTO = new RequestBlugPostDTO() { Body = "Body1", MetaDescription = "123456", Title = "123456", Tags = "123", MetaKeywords = "ff,hh" };

            //or Get Aplication Profile
            var Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();

            }).CreateMapper();
        
            IBlogPostService blogPostService = new BlogPostService(MockUnitOfWork.GetUnitOfWork().Object, Mapper, new RequestBlugPostDTOValidator());
            var res=  await blogPostService.CreatePostAsunc(requestBlugPostDTO);
           
            Assert.True(res.IsSuccess);
        }
    }
}
