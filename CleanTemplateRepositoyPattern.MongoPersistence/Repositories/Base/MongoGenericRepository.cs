using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb;
using CleanTemplateRepositoyPattern.Application.Models.MongoDb.Common;
using CleanTemplateRepositoyPattern.MongoPersistence.Configurations;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.MongoPersistence.Repositories.Base
{
    public class MongoGenericRepository<TEntity> :IMongoGenericRepository<TEntity> where TEntity : BaseModelMongo
    {
        protected readonly IMongoCollection<TEntity> DbSet;


        public MongoGenericRepository(MongoDbContext mongoContext)
        {
            this.DbSet = mongoContext.GetCollection<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                entity.Id = BsonObjectId.GenerateNewId().AsObjectId.ToString();
                await DbSet.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {

                throw new MongoClientException(ex.Message);
            }

        }
        

        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, int PageSize = 10, int PageNumber = 1)
        {

            var query = DbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);


            return await query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {


            var FilterBuilder = Builders<TEntity>.Filter.Eq(p => p.Id, id);
            var data = await DbSet.FindAsync(FilterBuilder);
            return data.SingleOrDefault();
        }



        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
        
            return all.ToList();
        }

        public virtual async Task<bool> UpdateOneAsync(TEntity entity)
        {
           



            try
            {
                var Result = await DbSet.ReplaceOneAsync<TEntity>(p => p.Id == entity.Id, entity);
                return Result.IsAcknowledged;
            }
            catch (Exception ex)
            {

                throw new MongoClientException(ex.Message);
            }


        }

        public virtual async Task<bool> DeleteOneAsync(string id)
        {

            try
            {
                var Result = await DbSet.DeleteOneAsync<TEntity>(p => p.Id == id);
                return Result.IsAcknowledged;
            }
            catch (Exception ex)
            {

                throw new MongoClientException(ex.Message);
            }


        }




    }
}
