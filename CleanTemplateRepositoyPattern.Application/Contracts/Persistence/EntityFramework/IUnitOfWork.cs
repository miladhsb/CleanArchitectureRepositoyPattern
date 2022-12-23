using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework
{
    public interface IUnitOfWork
    {
        IBlogCommentRepository BlogCommentRepository { get; }
        IBlogPostRepository BlogPostRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
