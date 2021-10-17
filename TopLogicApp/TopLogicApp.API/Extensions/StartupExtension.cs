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
using TopLogicApp.API.Middlewares;

namespace TopLogicApp.API.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigureAppJsonSerializer(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static void ConfigureAppDependencies(this IServiceCollection services,
            IConfiguration config)
        {
            var dbConnection = config.GetConnectionString(AppConstants.AppSetting_DefaultConnectionKey);

            services.AddScoped<IEmployeeService>(options => new EmployeeService(dbConnection));
        }

        public static void ConfigureAppAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingExtension));
        }

        public static void ConfigureAppExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options => {
                options.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandler?.Error;

                    if (exception != null) {
                        var error = JsonConvert.SerializeObject(new ExceptionDetails() {
                            ExceptionType = exception.GetType().Name,
                            Message = exception.Message,
                            DateRecorded = DateTime.UtcNow,
                            StackTrace = exception.StackTrace
                        });

                        await context.Response.WriteAsync(error);
                    }

                    // log to queue service
                });
            });
        }

        public static void ConfigureAppMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<TestMiddleware>();
        }
    }
}
