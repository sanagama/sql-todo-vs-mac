﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp
{
    public class Startup
    {
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
            // Add Entity Framework services for SQL Server.
            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<TodoAppContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:TodoAppDB"]));

			// Add MVC Core
            services.AddMvcCore().AddJsonFormatters();

            /*
            services.AddMvcCore().AddJsonOptions(options =>
			{
				// handle loops correctly
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

				// use standard name conversion of properties
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			});
			*/
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			// For demo purposes, we will drop and recreate the database at each run
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				TodoAppContext ctx = serviceScope.ServiceProvider.GetService<TodoAppContext>();

				// Drop & recreate the database at each run (we're not using EF DB migrations)
				ctx.Database.EnsureDeleted();
				ctx.Database.EnsureCreated();

				// Put some sample data into the database. See this: https://github.com/aspnet/EntityFramework/issues/3042
				ctx.EnsureSeedData();
			}

			app.UseMvc();
		}
	}
}