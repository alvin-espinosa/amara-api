using Amara.Microservice.Configuration.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Amara.Microservice.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IApplicationBuilder UseAmaraConfiguration(this IApplicationBuilder app,
            ConfigurationManager builderConfiguration)
        {
            app.UseHttpsRedirection();

            var origins = builderConfiguration.GetSection(Origins.Text).Get<string[]>() ?? new string[] { };

            app.UseCors(policy =>
            {
                policy.WithOrigins(origins);
                policy.AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
