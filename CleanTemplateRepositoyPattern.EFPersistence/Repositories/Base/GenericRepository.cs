using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.Domain.Common;
using CleanTemplateRepositoyPattern.EFPersistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Repositories.Base
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<TEntity> _Entity;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _Entity = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
    
                var result = await _dbContext.AddAsync(entity, cancellationToken);
                return result.Entity;
        }

        public TEntity SoftDelete(TEntity entity)
        {
    

                entity.IsDeleted = true;
                var result = _dbContext.Update(entity);
                return result.Entity;
        }


        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _Entity.AnyAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _Entity.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int PageNumber = 1,
            int PageSize = 15,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _Entity.AsQueryable();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);


            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);
            //if (select != null)
            //    query = query.Select(select);
            query = query.Skip((PageNumber - 1) * PageSize).Take(PageSize);
            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int PageNumber = 1,
            int PageSize = 15,
            CancellationToken cancellationToken = default
            )
        {
            IQueryable<TEntity> query = _Entity.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);
            //if (select != null)
            //    query = query.Select(select);

            query=query.Skip((PageNumber-1)* PageSize).Take(PageSize);
            return await query.ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _Entity.ToListAsync(cancellationToken);
        }



        public TEntity Update(TEntity entity)
        {
            var result = _Entity.Update(entity);
            return result.Entity;
        }


    }
}
