using AutoMapper;
using GeladosBH.Api.Configuration;
using GeladosBH.API.Configuration;
using GeladosBH.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeladosBH.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<GeladosBHContext>(options => options.UseInMemoryDatabase("GeladosBHDatabase"));

            services.AddIdentityConfiguration(Configuration);

            services.AddAutoMapper(typeof(AutomapperConfig));

            services.AddHttpClient();

            services.WebApiConfig();

            services.AddSwaggerConfig();

            services.ResolveDependencies();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("Development");
                

            }
            else if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("Staging");
                app.UseHttpsRedirection();
                app.UseHsts();

            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("Production");
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseSwaggerConfig(provider);
            app.UseAuthentication();
            app.UseMvcConfiguration();
        }
    }
}
