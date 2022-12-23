using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using CleanTemplateRepositoyPattern.EFPersistence.Configurations;
using CleanTemplateRepositoyPattern.EFPersistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Repositories
{
    public class BlogPostRepository:GenericRepository<BlogPost>,IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
