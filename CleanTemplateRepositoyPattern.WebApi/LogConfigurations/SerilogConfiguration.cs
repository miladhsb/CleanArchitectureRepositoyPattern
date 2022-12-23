using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.MongoDB;
using Serilog.Sinks.SystemConsole;
namespace CleanTemplateRepositoyPattern.WebApi.LogConfigurations
{
    public static class SerilogConfiguration
    {
        public static IHostBuilder AddSerilog(this WebApplicationBuilder app )
        {
           return app.Host.UseSerilog((Context, LogConfig) =>
            {
                if (Context.HostingEnvironment.IsDevelopment())
                {
                    // LogConfig.WriteTo.Console()
                    // .MinimumLevel.Information();

                    //LogConfig.WriteTo.MongoDB("mongodb://localhost:27017/SerilogDb", "SerilogDevelopment")
                    //.Enrich.WithProperty("CreateTime",DateTime.Now)
                    //.MinimumLevel.Information();
                   
                    LogConfig.WriteTo.Logger(p =>
                    {

                        p.Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore.Infrastructure"));

                        p.Filter.ByIncludingOnly(f2 => f2.Level >= LogEventLevel.Error);
                        p.WriteTo.Console();

                    });


                    LogConfig.WriteTo.Logger(p =>
                    {

                        p.Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command"));

                        p.Filter.ByIncludingOnly(f2 => f2.Level >= LogEventLevel.Warning);
                        p.WriteTo.Console();

                    });


                    LogConfig.WriteTo.Logger(p =>
                    {

                        p.Filter.ByIncludingOnly(Matching.FromSource("Microsoft.Hosting.Lifetime"));

                        p.Filter.ByIncludingOnly(f2 => f2.Level >= LogEventLevel.Information);
                        p.WriteTo.Console();

                    });


                    LogConfig.WriteTo.Logger(p =>
                    {

                        p.Filter.ByIncludingOnly(Matching.FromSource("CleanTemplateRepositoyPattern"));

                        p.Filter.ByIncludingOnly(f2 => f2.Level >= LogEventLevel.Information);
                        p.WriteTo.Console();

                    });


                    //log to mongo db and seq
                    LogConfig.WriteTo.Logger(p =>
                    {

                        //p.Filter.ByIncludingOnly(f => f.Level >= LogEventLevel.Information);
                        //p.WriteTo.MongoDB("mongodb://localhost:27017/SerilogDb", "SerilogDevelopment");
                        //p.Enrich.WithProperty("CreateTime", DateTime.Now);


                        //p.Filter.ByIncludingOnly(f => f.Level >= LogEventLevel.Information);
                        //p.WriteTo.Seq("http://localhost:5341");
                        //p.Enrich.WithProperty("CreateTime", DateTime.Now);
                         
                    });


                }
                if (Context.HostingEnvironment.IsProduction())
                {

                    //LogConfig.WriteTo.MongoDB("mongodb://localhost:27017/SerilogDb", "SerilogProduction")
                    //.Enrich.WithProperty("CreateTime", DateTime.Now)
                    //.MinimumLevel.Information();
                }


            });
          


        }
    }
}
