using Amara.Microservice.Configuration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amara.Microservice.Configuration
{
    public static class MicroserviceConfiguration
    {
        public static IServiceCollection AddMicroServiceConfiguration(
            this IServiceCollection services, 
            ConfigurationManager builderConfiguration)
        {
            var auth0 = builderConfiguration.GetSection(Constants.Auth0.Text).Get<Auth0>() ?? new Auth0();

            services
                .ConfigureAuthentication(auth0);
                //.ConfigureAuthorization();

            return services;
        }
    }
}
