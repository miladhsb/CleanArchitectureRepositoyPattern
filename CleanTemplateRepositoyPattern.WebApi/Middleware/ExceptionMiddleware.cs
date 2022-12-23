using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb;
using CleanTemplateRepositoyPattern.Application.Exceptions;
using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.Application.Utility;
using CleanTemplateRepositoyPattern.MongoPersistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CleanTemplateRepositoyPattern.WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
  

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
          
            try
            {

                await _next(httpContext);

            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = CreateErroreResult(context, exception, new List<ApplicationErrorResponse>() { new ApplicationErrorResponse() { Code = "01", Description = exception.Message } });



            switch (exception)
            {

                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ValidationModelException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = CreateErroreResult(context, exception, validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    break;
            }

            //جهت ثبت لاگ در مانگو دیبی
            //var res=  _mongoBsonDocumentRepository.AddOneDocumentAsync("ExceptionLogs",result).Result;

            //جهت ثبت لاگ توسط srilog
           var LogMessage = JsonSerializer.Deserialize<ApplicationExceptionError>(result);
            _logger.LogError(exception: exception, message: "LogMessage: {@LogMessage}", LogMessage);
            
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }

        private string CreateErroreResult(HttpContext context, Exception exception, List<ApplicationErrorResponse> ErrorMessages)
        {
            var option = new JsonSerializerOptions() {
            //Encoder= JavaScriptEncoder.Create(UnicodeRanges.Arabic, UnicodeRanges.BasicLatin, UnicodeRanges.c),
            Encoder= System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

            };
            var result = JsonSerializer.Serialize(new ApplicationExceptionError
            {
                TraceId = context.TraceIdentifier,
                ErrorMessages = ErrorMessages,
                HelpLink = exception.HelpLink,
                ExceptionSource = exception.Source,
                MemberName = exception.TargetSite.ReflectedType.Name,
                ExceptionType = exception.GetType().Name,
                HttpRoute= context.Request.Path.Value

            }, option);


            return result;
        }
    }



    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }


}
