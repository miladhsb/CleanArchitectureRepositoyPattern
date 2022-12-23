using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Configurations.Interceptor
{
    public class ApplicationDbCommandInterceptor: DbCommandInterceptor
    {
        private readonly ILogger<ApplicationDbCommandInterceptor> _logger;

        public ApplicationDbCommandInterceptor(IServiceScopeFactory serviceScope)
        {
            this._logger = serviceScope.CreateScope().ServiceProvider.GetRequiredService<ILogger<ApplicationDbCommandInterceptor>>();
        }
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            //Console.WriteLine(command.CommandText);
            _logger.LogInformation($"ApplicationDbCommandInterceptor: {command.CommandText}");
            return base.ReaderExecuted(command, eventData, result);
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {

            //eventData.Context.GetService<>
            _logger.LogInformation($"ApplicationDbCommandInterceptor: {command.CommandText}");
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }


    }
}
