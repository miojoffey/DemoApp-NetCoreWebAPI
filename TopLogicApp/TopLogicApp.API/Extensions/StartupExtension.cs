using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TopLogic.Core.Models;
using TopLogic.Services;
using TopLogic.Services.Interfaces;
using TopLogicApp.API.DTO;
using TopLogicApp.API.Middlewares;

namespace TopLogicApp.API.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigureAppJsonSerializer(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public static void ConfigureAppDependencies(this IServiceCollection services,
            IConfiguration config)
        {
            // employee
            var dbConnection = config.GetConnectionString(AppConstants.AppSetting_DefaultConnection);
            services.AddScoped<IEmployeeService>(options => new EmployeeService(dbConnection));
        }

        public static void ConfigureAppAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingExtension));
        }

        public static void ConfigureAppExceptionHandler(this IApplicationBuilder app,
            IConfiguration config)
        {
            app.UseExceptionHandler(options => {

                // azure storage queue
                var azureStorageQueue = config
                    .GetSection(AppConstants.AppSetting_AzureStorageQueue)
                    .Get<AzureStorageQueueDTO>();

                // error log service
                var errorLogService = new ErrorLogService(
                    azureStorageQueue.Connection,
                    azureStorageQueue.ErrorLogQueueName);

                options.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandler?.Error;

                    if (exception != null) {
                        var error = new ExceptionDetails() {
                            StatusCode = context.Response.StatusCode,
                            ExceptionType = exception.GetType().Name,
                            Message = exception.Message,
                            DateRecorded = DateTime.UtcNow
                        };

                        // write to response
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(error));

                        // write to log
                        error.StackTrace = exception.StackTrace;
                        await errorLogService.WriteAsync(error);
                    }                    
                });
            });
        }

        public static void ConfigureAppMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<TestMiddleware>();
        }
    }
}
