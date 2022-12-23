using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Configurations
{
    //public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        //IConfigurationRoot configuration = new ConfigurationBuilder()
    //        //    .SetBasePath(Directory.GetCurrentDirectory())
    //        //    .AddJsonFile("appsettings.json")
    //        //    .Build();
    //        //Console.WriteLine(configuration.GetConnectionString("CleanApp"));
    //        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        var connectionString = "Data Source=.;Initial Catalog=CleanAppDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True";


    //        builder.UseSqlServer(connectionString);

    //        return new ApplicationDbContext(builder.Options);
    //    }
    //}
}
