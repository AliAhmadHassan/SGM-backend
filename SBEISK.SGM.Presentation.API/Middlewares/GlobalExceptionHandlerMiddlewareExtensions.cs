using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SBEISK.SGM.Presentation.API.Middlewares
{
    public static class GlobalMiddlewareExtensions
    {
        public static IServiceCollection AddGlobalMiddleware(this IServiceCollection services)
        {
            return services.AddTransient<GlobalMiddleware>();
        }

        public static void UseGlobalMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalMiddleware>();
        }
    }
}
