using Api_Fatoracao.Config;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped((context) =>
            {
                return Logger.Factory.Get();
            });

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

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

            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options);
        }

        private void RegisterKissLogListeners(IOptionsBuilder options)
        {
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                Configuration["KissLog.OrganizationId"],
                Configuration["KissLog.ApplicationId"])
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]
            });
        }
    }
}
