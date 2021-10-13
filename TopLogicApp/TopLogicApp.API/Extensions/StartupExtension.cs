using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopLogic.Services;
using TopLogic.Services.Interfaces;

namespace TopLogicApp.API.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigureAppDependencies(this IServiceCollection services,
            IConfiguration config)
        {
            var dbConnection = config.GetConnectionString(AppConstants.AppSettings_DefaultConnectionKey);

            services.AddScoped<IEmployeeService>(options => new EmployeeService(dbConnection));
        }

        public static void ConfigureAppAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingExtension));
        }
    }
}
