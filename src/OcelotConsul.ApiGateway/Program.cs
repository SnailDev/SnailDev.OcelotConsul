using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using OcelotConsul.ApiGateway.Core;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;

namespace OcelotConsul.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "OcelotConsul.ApiGateway";
            BuildWebHost(args).Run();

            //new WebHostBuilder()
            //.UseKestrel()
            //.UseContentRoot(Directory.GetCurrentDirectory() + @"\bin\debug\netcoreapp2.1")
            //.ConfigureAppConfiguration(conbuilder =>
            //{
            //    conbuilder.AddCommandLine(args);
            //    conbuilder.AddJsonFile("appsettings.json");
            //    conbuilder.AddJsonFile("configuration.json");
            //})
            //.ConfigureServices(s =>
            //{
            //    s.AddOcelot();
            //})
            //.ConfigureLogging((hostingContext, logging) =>
            //{
            //    logging.AddConsole();
            //    logging.AddDebug();
            //})
            //.UseIISIntegration()
            //.Configure(app =>
            //{
            //    app.UseOcelot().Wait();
            //})
            //.Build()
            //.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder builder = new WebHostBuilder();
            //注入WebHostBuilder
            return builder.ConfigureServices(service =>
            {
                service.AddSingleton(builder);
            })
            .ConfigureAppConfiguration(conbuilder =>
            {
                conbuilder.AddCommandLine(args);
                conbuilder.AddJsonFile("appsettings.json");
                conbuilder.AddJsonFile("configuration.json");
            })
            .UseContentRoot(Directory.GetCurrentDirectory() + @"\bin\debug\netcoreapp2.1")
            .UseKestrel()
            .UseUrls("http://*:5000")
            .UseStartup<Startup>()
            .Build();
        }
    }
}
