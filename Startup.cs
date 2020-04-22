using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CloudDownloader
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var xmlpath = Path.Combine(Path.GetDirectoryName(typeof(Startup).Assembly.Location), Assembly.GetEntryAssembly().GetName().Name + ".xml");
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CloudDownloader", Version = "v1", Description = "No Description" });
                c.IncludeXmlComments(xmlpath);
            });//配置Swagger
            services.Configure<IConfiguration>(Configuration);
            services.AddSignalR().AddNewtonsoftJsonProtocol();
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));//配置Swagger
            loggerFactory.AddFile("Logs/{Date}.log");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => endpoints.MapGet("/", async context => await context.Response.WriteAsync("Hello World!")));
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
            //app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }//End Class
}
