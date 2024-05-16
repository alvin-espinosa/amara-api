using Amara.Microservice.Configuration.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Amara.Microservice.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration<T>(
            this IServiceCollection services,
            ConfigurationManager builderConfiguration) where T : DbContext
        {
            var dbConfig = builderConfiguration.GetSection(Constants.DBConfig.Text).Get<DBConfig>() ?? new DBConfig();

            services.AddDbContext<T>(opt =>
            {
                var _writer = new StreamWriter("efcore-log.txt", true);

                opt
                    .UseSqlServer($"Server={dbConfig.Host};Database=${dbConfig.Name};Trusted_Connection=True;TrustServerCertificate=True;Encrypt=True")
                    .LogTo(
                        _writer.WriteLine, //Console.WriteLine, 
                        new[]
                        {
                            DbLoggerCategory.Database.Command.Name
                        },
                        LogLevel.Debug)
                    .EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            return services;
        }

        public static void UseDatabaseConfiguration<T>(this WebApplication app) where T : DbContext
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();
                context.Database.Migrate();
            }
        }
    }
}
