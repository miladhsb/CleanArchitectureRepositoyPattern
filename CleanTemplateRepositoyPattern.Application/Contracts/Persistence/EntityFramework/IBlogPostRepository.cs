using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework
{
    public interface IBlogPostRepository : IGenericRepository<BlogPost>
    {
    }
}
