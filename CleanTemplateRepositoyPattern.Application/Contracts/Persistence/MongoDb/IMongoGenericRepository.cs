using CleanTemplateRepositoyPattern.Application.Models.MongoDb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb
{
    public interface IMongoGenericRepository<TEntity> where TEntity : BaseModelMongo
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> DeleteOneAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, int PageSize = 10, int PageNumber = 1);
        Task<TEntity> GetByIdAsync(string id);
        Task<bool> UpdateOneAsync(TEntity entity);
    }
}
