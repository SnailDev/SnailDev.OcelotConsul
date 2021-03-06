﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotConsul.ApiGateway.Core.DelegatingHandlers;
using System;
using System.Threading.Tasks;

namespace OcelotConsul.ApiGateway.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot()
                // .AddSingletonDelegatingHandler<TracingHandler>(true)
                // .AddStoreOcelotConfigurationInConsul()
                .AddOpenTracing(option =>
                {
                    option.CollectorUrl = Configuration["ButterflyCollectorUrl"] ?? "http://localhost:9618";
                    option.Service = Configuration["ServiceName"] ?? "OcelotConsul.ApiGateway";
                });

            // services.AddOcelot().AddAdministration("/administration", "secret");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOcelot();
        }
    }
}
