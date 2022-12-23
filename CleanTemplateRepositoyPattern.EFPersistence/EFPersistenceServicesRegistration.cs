using CleanTemplateRepositoyPattern.Application.Constants;
using CleanTemplateRepositoyPattern.Application.Contracts.Persistence;
using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.EFPersistence.Configurations;
using CleanTemplateRepositoyPattern.EFPersistence.Repositories;
using CleanTemplateRepositoyPattern.EFPersistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence
{
    public static class EFPersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DbConnectionStringName.EfSqlServer),
            x=>x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));


            

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region درصورت استفاده نکردن از یونیت اف ورک باید از این کدها استفاده نمایید
            // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            //services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            #endregion


            #region ایجاد مایگریشن در صورتی که در دیتابیس اعمال نشده باشد
            using (var servicescope = services.BuildServiceProvider().CreateScope())
            {
                var dbcontext = servicescope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


                if (dbcontext.Database.GetPendingMigrationsAsync().Result.Any())
                {
                    dbcontext.Database.MigrateAsync().Wait();
                }
            }
            #endregion


            return services;
        }
    }
}
