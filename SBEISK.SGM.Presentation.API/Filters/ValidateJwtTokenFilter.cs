using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SBEISK.SGM.CrossCutting.Configurations;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Presentation.API.Attributes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SBEISK.SGM.Presentation.API.Filters
{
    public class ValidateJwtTokenFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Empty method, won't do anything after pipeline execution
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            JwtTokenConfiguration config = ((IOptions<JwtTokenConfiguration>)context.HttpContext.RequestServices.GetService(typeof(IOptions<JwtTokenConfiguration>))).Value;
            IFilterMetadata attribute = context.ActionDescriptor.FilterDescriptors
                .FirstOrDefault(x => x.Filter is ValidateJwtActionTokenAttribute || x.Filter is ValidateJwtTokenAttribute)?.Filter;

            if (attribute == null)
            {
                return;
            }

            bool hasToken = context.HttpContext.Request.Query["token"].Any();
            if (!hasToken)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string token = context.HttpContext.Request.Query["token"].First();
            string permission = attribute is ValidateJwtActionTokenAttribute ? (attribute as ValidateJwtActionTokenAttribute).Permission.ToString("d") : string.Empty;
            if (attribute is ValidateJwtActionTokenAttribute)
            {              
                if (!ValidateToken(token, permission, config))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

        private bool ValidateToken(string authToken, string permission, JwtTokenConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(configuration);
            try
            {
                ClaimsPrincipal identity = tokenHandler.ValidateToken(authToken, validationParameters, out _);
                if (string.IsNullOrEmpty(permission))
                {
                    return identity.Identity.IsAuthenticated;
                }

                return identity.HasClaim(x => x.Type == "Permission" && x.Value.ToString() == permission);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private TokenValidationParameters GetValidationParameters(JwtTokenConfiguration configuration)
        {
            return new TokenValidationParameters()
            {
                ValidIssuer = configuration.Issuer,
                ValidAudience = configuration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key))
            };
        }
    }
}
