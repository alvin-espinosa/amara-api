using Amara.Microservice.Configuration.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Amara.Microservice.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, Auth0 auth0)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = auth0.Domain;
                    options.Audience = auth0.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = auth0.Domain,
                        //ValidateIssuer = auth0secret != null,
                        ValidateAudience = true,
                        ValidAudience = auth0.Audience,
                        ValidateIssuerSigningKey = true,
                        NameClaimType = ClaimTypes.NameIdentifier,
                        //IssuerSigningKey = secret != null ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)) : null
                    };
                });

            return services;
        }
    }
}
