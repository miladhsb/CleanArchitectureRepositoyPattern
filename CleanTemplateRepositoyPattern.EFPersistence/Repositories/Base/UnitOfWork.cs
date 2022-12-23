using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.EFPersistence.Configurations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Repositories.Base
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBlogPostRepository _blogPostRepository;
        private IBlogCommentRepository _blogCommentRepository;
        public UnitOfWork(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IBlogPostRepository BlogPostRepository => _blogPostRepository ?? new BlogPostRepository(_dbContext);
        public IBlogCommentRepository BlogCommentRepository => _blogCommentRepository ?? new BlogCommentRepository(_dbContext);


        
        public async Task<int> SaveChangesAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            

            return await _dbContext.SaveChangesAsync(Guid.Parse(userId??= Guid.Empty.ToString()));
        }


    }
}
