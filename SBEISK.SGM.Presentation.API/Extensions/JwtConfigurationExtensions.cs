using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SBEISK.SGM.Domain.Permissions;
using System;
using System.Linq;
using System.Text;

namespace SBEISK.SGM.Presentation.API.Extensions
{
    public static class JwtConfigurationExtensions
    {
        public static void AddJwtValidation(this IServiceCollection services, IConfiguration configurations)
        {
            AddAuthentication(services, configurations);
            AddAuthorization(services);
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configurations)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                TokenValidationParameters paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations["TokenConfigurations:Key"]));
                paramsValidation.ValidAudience = configurations["TokenConfigurations:Audience"];
                paramsValidation.ValidIssuer = configurations["TokenConfigurations:Issuer"];
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (ActionPermissions permission in Enum.GetValues(typeof(ActionPermissions)).Cast<ActionPermissions>())
                {
                    string permissionName = permission.ToString("g");
                    options.AddPolicy(permissionName, policy => policy.RequireClaim("Permission", permission.ToString("d")));
                }
            });

            services.AddAuthorization(options =>
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin", "1")
                ));
        }
    }
}
