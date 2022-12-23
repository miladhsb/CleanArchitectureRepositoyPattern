using CleanTemplateRepositoyPattern.Application.Contracts.Email;
using CleanTemplateRepositoyPattern.Infrastructure.Mail;
using CleanTemplateRepositoyPattern.Infrastructure.SettingModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<SmtpSetting>(configuration.GetSection("SmtpSetting"));
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            return services;
        }
    }
}
