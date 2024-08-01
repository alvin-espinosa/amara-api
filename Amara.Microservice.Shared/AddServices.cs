using Amara.Microservice.Shared.DTOs;
using Amara.Microservice.Shared.Interface;
using Amara.Microservice.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Amara.Microservice.Shared
{
    public static class AddServices
    {
        public static IServiceCollection AddEmailServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services
                .AddOptions<EmailProvider>()
                .Configure<IConfiguration>((options, configuration) =>
                    configuration.GetSection(EmailProvider.Key).Bind(options));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }

        public static IServiceCollection AddCacheServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddOptions<CacheConfig>()
                .Configure<IConfiguration>((options, configuration) =>
                    configuration.GetSection(CacheConfig.Key).Bind(options));

            var redis = new CacheConfig();

            configuration.GetSection(CacheConfig.Key).Bind(redis);

            services.AddSingleton<IConnectionMultiplexer>(cm =>
            {
                return ConnectionMultiplexer.Connect(redis.ConnectionString);
            });

            services.AddTransient<ICacheService, CacheService>();

            return services;
        }
    }
}
