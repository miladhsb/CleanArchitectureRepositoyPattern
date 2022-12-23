using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb;
using CleanTemplateRepositoyPattern.MongoPersistence.Configurations;
using CleanTemplateRepositoyPattern.MongoPersistence.Repositories;
using CleanTemplateRepositoyPattern.MongoPersistence.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.MongoPersistence
{
    public static class MongoPersistenceServicesRegistration
    {
        public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MongoDbSetting>(configuration.GetSection("MongoDbSetting"));

            services.AddSingleton<MongoDbContext>();
            services.AddScoped(typeof(IMongoGenericRepository<>), typeof(MongoGenericRepository<>));
            services.AddScoped<IMongoBsonDocumentRepository, MongoBsonDocumentRepository>();

            return services;
        }

    }
}
