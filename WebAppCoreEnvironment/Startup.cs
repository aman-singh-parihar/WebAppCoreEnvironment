using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreEnvironment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsEnvironment("Development-IISExpress"))
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync($"Envrionment Name: { env.EnvironmentName}");
                });
            }
            app.Run(async context =>
            {
                await context.
                Response.
                WriteAsync($"Envrionment Name: {env.EnvironmentName}{Environment.NewLine}" +
                $"Staging: {env.IsStaging()}{Environment.NewLine}" +
                $"Production: {env.IsProduction()}{Environment.NewLine}" +
                $"Development: {env.IsDevelopment()}{Environment.NewLine}" +
                $"Full Object: {env}{Environment.NewLine}" +
                $"Other Envrionment Variable: {Environment.GetEnvironmentVariable("LOCAL_DEVELOPMENT")}");//local developement of IISExpress profile

                
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
