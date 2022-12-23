using CleanTemplateRepositoyPattern.EFPersistence.Configurations;
using CleanTemplateRepositoyPattern.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureRepositoyPattern.IntegrationTests.Configurations
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public CustomWebApplicationFactory()
        {
          
        }
        protected override void ConfigureClient(HttpClient client)
        {
           
            base.ConfigureClient(client);
            client.BaseAddress = new Uri("http://localhost/api/");
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            

            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });
         
            builder.ConfigureServices((builder, services) =>
            {
        

                //services.Remove<DbContextOptions<ApplicationDbContext>>()
                //    .AddDbContext<ApplicationDbContext>((sp, options) =>
                //        options.UseSqlServer(builder.Configuration.GetConnectionString("CleanApp"),
                //            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            });
        }
    }
}
