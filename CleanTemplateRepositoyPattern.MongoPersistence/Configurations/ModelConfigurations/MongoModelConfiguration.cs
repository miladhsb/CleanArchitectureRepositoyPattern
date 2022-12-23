using CleanTemplateRepositoyPattern.Application.Models.MongoDb;
using CleanTemplateRepositoyPattern.Application.Models.MongoDb.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.MongoPersistence.Configurations.ModelConfigurations
{
    public static class MongoModelConfiguration
    {
        public static void InitModelConfiguration()
        {
            BsonClassMap.RegisterClassMap<BaseModelMongo>(cm =>
            {

                //cm.MapIdProperty(c => c.Id).SetIdGenerator(ObjectIdGenerator.Instance).SetSerializer(new ObjectIdSerializer(BsonType.ObjectId));
                cm.MapIdProperty(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));

            });

            BsonClassMap.RegisterClassMap<AplicationHistory>(cm =>
            {
                cm.MapProperty(c => c.Creator).SetSerializer(new StringSerializer(BsonType.String));
                cm.MapProperty(c => c.CreateTime).SetSerializer(new DateTimeSerializer(BsonType.DateTime));
                cm.MapProperty(c => c.log).SetSerializer(new StringSerializer(BsonType.String));
            });
        }
    }
}
