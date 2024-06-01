using GeladosBH.API.Extensions;
using GeladosBH.Business.Intefaces;
using GeladosBH.Core.Services;
using GeladosBH.Data;
using GeladosBH.Data.Repository;
using GeladosBH.Domain.Interfaces;
using GeladosBH.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GeladosBH.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            //Swagger
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


            //Context
            services.AddScoped<GeladosBHContext>();

            //Notifications
            services.AddScoped<INotificadorService, NotificadorService>();


            return services;
        }
    }
}
