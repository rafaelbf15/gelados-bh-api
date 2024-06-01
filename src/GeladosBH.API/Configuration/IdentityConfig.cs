using GeladosBH.Api.Extensions;
using GeladosBH.API.Data;
using GeladosBH.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GeladosBH.API.Configuration
{
    public static class IDentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("GeladosBHDatabase_Identity"));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();

            //JWT
            var appSettingsConfigSection = configuration.GetSection("AppSettingConfig");
            services.Configure<AppSettingsConfig>(appSettingsConfigSection);

            var appSettingsConfig = appSettingsConfigSection.Get<AppSettingsConfig>();
            var key = Encoding.ASCII.GetBytes(appSettingsConfig.Secret);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettingsConfig.ValidoEm,
                    ValidIssuer = appSettingsConfig.Emissor
                };
            });

            return services;
        }
    }
    
}
