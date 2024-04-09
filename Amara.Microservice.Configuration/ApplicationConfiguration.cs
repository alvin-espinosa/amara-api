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
                policy.WithOrigins("http://localhost:4200");
                policy.AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
