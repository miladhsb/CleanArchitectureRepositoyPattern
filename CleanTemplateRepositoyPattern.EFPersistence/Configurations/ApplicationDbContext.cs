using CleanTemplateRepositoyPattern.Domain.Common;
using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using CleanTemplateRepositoyPattern.EFPersistence.Configurations.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Configurations
{
    public class ApplicationDbContext:DbContext
    {
        private readonly IServiceScopeFactory _serviceScope;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IServiceScopeFactory serviceScope) :base(options)
        {
            this._serviceScope = serviceScope;
        }

        #region DbSets
        public DbSet<BlogComment> blogComments { get; set; }
        public DbSet<BlogPost> blogPosts { get; set; }
        #endregion

        #region Configuring
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

             optionsBuilder.AddInterceptors(new ApplicationDbCommandInterceptor(_serviceScope));

           
            //جهت لاگ کوئری های دیتابیس
            //optionsBuilder.LogTo(p =>
            //{
            //    //Console.WriteLine(p);
            //},Microsoft.Extensions.Logging.LogLevel.Information);



            base.OnConfiguring(optionsBuilder);


        }
        #endregion



        #region Save Change

        public virtual async Task<int> SaveChangesAsync(Guid UserId)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
             

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = UserId;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = UserId;

                }

            }

            var result = await base.SaveChangesAsync();

            return result;
        }
        #endregion

       



    }
}
