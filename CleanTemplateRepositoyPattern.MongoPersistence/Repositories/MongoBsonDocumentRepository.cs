using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb;
using CleanTemplateRepositoyPattern.MongoPersistence.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanTemplateRepositoyPattern.MongoPersistence.Repositories
{
    public class MongoBsonDocumentRepository : IMongoBsonDocumentRepository
    {
        private readonly MongoDbContext _mongoContext;

        public MongoBsonDocumentRepository(MongoDbContext mongoContext)
        {
            this._mongoContext = mongoContext;
        }



        public async Task<bool> AddOneDocumentAsync(string CollectionName, string JsonDocument)
        {
            if (string.IsNullOrEmpty(CollectionName))
            {
                throw new ArgumentNullException(" CollectionName نمیتواند خالی باشد");
            }

            IMongoCollection<BsonDocument> DbSet = _mongoContext.GetBsonCollection(CollectionName);

            if (string.IsNullOrEmpty(JsonDocument))
            {
                throw new ArgumentNullException("JsonDocument نمیتواند خالی باشد");
            }
      

                 BsonDocument BsonDoc = BsonDocument.Parse(JsonDocument);
        
              
                BsonDoc.SetElement(new BsonElement("_id", new BsonObjectId(ObjectId.GenerateNewId())));
                
               
                
                await DbSet.InsertOneAsync(BsonDoc);
            return true;




        }
        public async Task<List<TEntity>> GetDocumentAllAsync<TEntity>(string CollectionName)
        {
            if (string.IsNullOrEmpty(CollectionName))
            {
                throw new ArgumentNullException(" CollectionName نمیتواند خالی باشد");
            }

            BsonClassMap.RegisterClassMap<TEntity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty("_id").SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.SetIgnoreExtraElements(true);
            });

            IMongoCollection<TEntity> DbSet = _mongoContext.GetCollectionByName<TEntity>(CollectionName);
            var result = await DbSet.AsQueryable().ToListAsync();


            return result;

        }



       

       

    }
}
