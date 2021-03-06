﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Data;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = @"Server=localhost;Database=VideoGames;User Id=sa;Password=sujay";
            services.AddDbContext<Context>(options => {
                options.UseSqlServer("Server=localhost;Database=VideoGames;User Id=sa;Password=sujay");
                //options.UseSqlite("Filename=./VideoGames.db");//"Data Source=VideoGames.db");
            });



            //Enable CORS.
            services.AddCors();
            

             var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            //corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
            //corsBuilder.AllowCredentials();
            
            //https://stackoverflow.com/questions/40043097/cors-in-net-core
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            // services.Configure<MvcOptions>(options => 
            // {
            //      options.Filters.Add(new CorsAuthorizationFilterFactory
            //      ("AllowSpecificOrigin"));   

            // });


            // Add framework services.
            services.AddMvc()
                    .AddJsonOptions(options => 
                        options.SerializerSettings.ReferenceLoopHandling = 
                            ReferenceLoopHandling.Ignore);

            //services.AddAutoMapper(typeof(Startup));                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,Context context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
                
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            // }

            //order imp
            app.UseDefaultFiles();
            
            app.UseStaticFiles();

            app.UseCors("SiteCorsPolicy");

            // app.UseCors(builder =>
            //      builder.WithOrigins("http://localhost:4200"));
                
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //if(env.IsDevelopment())
            {
               context.Initialize();     
            }
        }
    }
}
