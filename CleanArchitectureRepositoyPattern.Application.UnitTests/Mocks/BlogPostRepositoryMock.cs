using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using CleanTemplateRepositoyPattern.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureRepositoyPattern.Application.UnitTests.Mocks
{
    public static class BlogPostRepositoryMock
    {
        public static Mock<IBlogPostRepository> GetBlogPostRepository()
        {
            List<BlogPost> blogPosts = new List<BlogPost>()
            {
                new BlogPost(){
                AllowComments = true,
                Title = "Title",
                Body = "Body",
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDate = DateTime.Now,
                MetaDescription = "MetaDescription",
                MetaKeywords = "MetaKeywords",
                MetaTitle = "Title",
                PostImage = "",
                PostSlug = "PostSlug",
                postState = PostState.published,
                Tags = "123" },

                     new BlogPost(){
                AllowComments = true,
                Title = "Title2",
                Body = "Body2",
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDate = DateTime.Now,
                MetaDescription = "MetaDescription2",
                MetaKeywords = "MetaKeywords2",
                MetaTitle = "Title2",
                PostImage = "",
                PostSlug = "PostSlug2",
                postState = PostState.published,
                Tags = "123" },


            };

            Mock<IBlogPostRepository> mockIBlogPostRepository = new Mock<IBlogPostRepository>();
            mockIBlogPostRepository.Setup(p => p.GetAllAsync(default)).ReturnsAsync(blogPosts);

           
            mockIBlogPostRepository.Setup(p => p.AddAsync(It.IsAny<BlogPost>(), It.IsAny<CancellationToken>())).ReturnsAsync((BlogPost blogPost,CancellationToken Ct ) =>
            {
                blogPosts.Add(blogPost);
                return blogPost;
            });
            return mockIBlogPostRepository;
        }
    
        

    }
    
}
