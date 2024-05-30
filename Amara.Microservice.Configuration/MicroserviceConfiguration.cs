using Amara.Microservice.Configuration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

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
                .ConfigureAuthentication(auth0)
                //.ConfigureAuthorization();
                .AddMvc()
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            return services;
        }
    }
}
