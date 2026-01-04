using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using SBEISK.SGM.CrossCutting.Configurations;
using SBEISK.SGM.CrossCutting.IoC;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Presentation.API.Extensions;
using SBEISK.SGM.Presentation.API.Filters;
using SBEISK.SGM.Presentation.API.Middlewares;
using System.Linq;
using System.Reflection;
using WebApiContrib.Core.Formatter.Csv;

namespace SBEISK.SGM.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<LdapConfig>(Configuration.GetSection("Ldap"));
            services.Configure<FileSystem>(Configuration.GetSection("FileSystem"));
            services.Configure<JwtTokenConfiguration>(Configuration.GetSection("TokenConfigurations"));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(new ValidateJwtTokenFilter());

                config.OutputFormatters.Add(new CsvOutputFormatter(new CsvFormatterOptions() { CsvDelimiter = ",", Encoding = System.Text.Encoding.UTF8 }));
                config.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/csv" });
            });

            services.AddDbContext<SgmDataContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("Default"));
                builder.EnableSensitiveDataLogging();
            });
            services.AddResponseCaching();
            services.AddJwtValidation(Configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSwagger();
            services.AddGlobalMiddleware();
            NativeDotNetInjector.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSwaggerRoutes(env);
            app.UseAuthentication();
            app.UseGlobalMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
