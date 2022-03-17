using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyCoreDemo2
{
    public class Startup
    {
        private IConfiguration _config;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("Mywebpage.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            //app.UseStaticFiles();
            FileServerOptions file = new FileServerOptions();
            file.DefaultFilesOptions.DefaultFileNames.Clear();
            file.DefaultFilesOptions.DefaultFileNames.Add("Mywebpage.html");

            app.UseFileServer(file);
           
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1 : Incoming request\n");
                await next();
                await context.Response.WriteAsync("Middleware 1 :Outgoing Response\n");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 2 : Incoming Request \n");
                await next();
                await context.Response.WriteAsync("Middleware 12:Outgoing Response\n");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 3 : Incoming request\n");
                await next();
                await context.Response.WriteAsync("Middleware 3 :Outgoing Response\n");
            });
            app.Run(async context =>
            {
                await context.Response.WriteAsync(" Hello from Terminal middleware\n");
            });
            
            //Terminal Middleware
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        //await context.Response.WriteAsync( "Current Process Name "
            //        //    +System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            //        await context.Response.WriteAsync(_config["MycustomKey"]);

            //    });
            //});


        }
    }
}
