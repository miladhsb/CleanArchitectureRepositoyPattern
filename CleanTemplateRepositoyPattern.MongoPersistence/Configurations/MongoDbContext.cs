using CleanTemplateRepositoyPattern.Application.Models.MongoDb.Common;
using CleanTemplateRepositoyPattern.MongoPersistence.Configurations.ModelConfigurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.MongoPersistence.Configurations
{
    class myprop
    {
        public string name { get; set; }
    }
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly ILogger<MongoDbContext> _logger;

        public MongoDbContext(IOptions<MongoDbSetting> options,ILogger<MongoDbContext> logger)
        {
            this._logger = logger;
            //var connectionString = "mongodb://user1:password1@localhost/test";

            //جهت کانفیگ مدلهای مانگو
            MongoModelConfiguration.InitModelConfiguration();

            IMongoClient mongoClient = new MongoClient(new MongoClientSettings()
            {
                Server = new MongoServerAddress(options.Value.ServerIP, 27017),
                //جهت تنظیمات امنیتی مانگو دیبی
                //Credential=MongoCredential.CreateCredential(options.Value.Database, options.Value.UserName, options.Value.Password),

                ConnectTimeout = new TimeSpan(0, 0, 5),

                ClusterConfigurator = (p) =>
                {
                    p.Subscribe<CommandStartedEvent>(s =>
                    {
                        // Console.WriteLine($"Log mongodb CommandName : {s.CommandName} CommandDetail :{s.Command.ToJson()}");

                        //جهت ثبت کامند لاگ های مانگودیبی 
                    
                            _logger.LogInformation(message: $"Log mongodb CommandName : {s.CommandName} CommandDetail :{s.Command.ToJson()} ");


                        //جهت ثبت لاگ با پراپرتی های سفارشی
                        //using (_logger.BeginScope(new Dictionary<string, object> { { "LogGroup", "MongoDbContext" } }))
                        //{

                        //   _logger.LogInformation(message: $"Log mongodb CommandName : {s.CommandName} CommandDetail :{s.Command.ToJson()} ");

                        //}


                    });

                },



            });
            _mongoDatabase = mongoClient.GetDatabase(options.Value.Database);
            
        }


        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {

            return _mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);



        }
        public IMongoCollection<TEntity> GetCollectionByName<TEntity>(string CollectionName)
        {

            return _mongoDatabase.GetCollection<TEntity>(CollectionName);

        }
        public IMongoCollection<BsonDocument> GetBsonCollection(string CollectionName)
        {

            return _mongoDatabase.GetCollection<BsonDocument>(CollectionName);

        }

        //#region Collection
        //public IMongoCollection<Customer> Customers { get => _mongoDatabase.GetCollection<Customer>("Customers"); }

        //#endregion



    }

}
