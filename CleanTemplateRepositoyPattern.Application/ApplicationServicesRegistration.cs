using CleanTemplateRepositoyPattern.Application.Constants.localizationMessages;
using CleanTemplateRepositoyPattern.Application.Services.BlogPostService;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBaseMessages, MessagesIRFa>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            #region FluentValidation service
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("fa");

            #endregion

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;

        }
        
    }
}
