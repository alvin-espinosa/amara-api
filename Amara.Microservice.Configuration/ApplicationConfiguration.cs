using Microsoft.AspNetCore.Builder;

namespace Amara.Microservice.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IApplicationBuilder UseAmaraConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseCors(policy =>
            {
                policy.WithOrigins("http://localhost:4200", "https://kadi-3.azurewebsites.net");
                policy.AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
