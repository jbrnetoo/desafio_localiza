using Api_Fatoracao.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api_Fatoracao
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(hostEnvironment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddRouting();

            services.AddCors();

            VersioningConfig.Register(services);

            SwaggerConfig.Register(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentação");
                c.InjectStylesheet("/swagger/theme-flattop.css");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
