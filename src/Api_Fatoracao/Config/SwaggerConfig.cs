using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Api_Fatoracao.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API Faturacao",
                    Version = "v1",
                    Description = "Desafio sugerido pela Localiza"
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Api_Faturacao.xml");
                c.IncludeXmlComments(filePath);
            });

            return services;
        }
    }
}
