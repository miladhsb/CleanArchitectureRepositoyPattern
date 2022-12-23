using CleanTemplateRepositoyPattern.Application;
using CleanTemplateRepositoyPattern.Application.Exceptions;
using CleanTemplateRepositoyPattern.EFPersistence;
using CleanTemplateRepositoyPattern.Identity;
using CleanTemplateRepositoyPattern.Infrastructure;
using CleanTemplateRepositoyPattern.MongoPersistence;
using CleanTemplateRepositoyPattern.WebApi.Common;
using CleanTemplateRepositoyPattern.WebApi.LogConfigurations;
using CleanTemplateRepositoyPattern.WebApi.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace CleanTemplateRepositoyPattern.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region سفارشی سازی پیام های بد ریکوئست api
            //builder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressInferBindingSourcesForParameters = true;
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
            //        throw new BadRequestException("eeeeeee");
            //        //return new BadRequestObjectResult(new
            //        //{
            //        //    Code = errors,
            //        //    Message = "Validation errors",
            //        //  //  Errors = errors
            //        //});
            //    };
            //});
            #endregion


            builder.AddSerilog();


        


            builder.Services.AddControllers();
            //    .AddFluentValidation(options =>
            //{
            //    // Validate child properties and root collection elements
            //    options.ImplicitlyValidateChildProperties = true;
            //    options.ImplicitlyValidateRootCollectionElements = true;
            //    options.DisableDataAnnotationsValidation = true;
            //    // Automatic registration of validators in assembly
            //    options.RegisterValidatorsFromAssembly(Assembly.Load("CleanTemplateRepositoyPattern.Application"));
            //}); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerWithAuth();

            #region Add_Asp.net_Core_Service
            builder.Services.AddHttpContextAccessor();
            #endregion

            #region Add_Application_Service
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddMongoDbServices(builder.Configuration);
            builder.Services.InfrastructureServices(builder.Configuration);
            #endregion

            #region AddCors
            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("AppCorsPolicy", CorsBuilder => CorsBuilder
                    .WithOrigins("http://localhost:8001", "http://127.0.0.1:5003")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("*")
                    );
            });
            #endregion


            var app = builder.Build();
            
         


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //جهت نمایش پیغام های خطا به صورت سفارشی
            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();
           
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AppCorsPolicy");
            app.MapControllers();

            app.Run();
        }



    }
}