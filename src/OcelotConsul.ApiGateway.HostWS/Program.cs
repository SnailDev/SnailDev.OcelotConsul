using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using OcelotConsul.ApiGateway.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.Linq;

namespace OcelotConsul.ApiGateway.HostWS
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isService = true;
            if (Debugger.IsAttached || args.Contains("--console"))
            {
                Console.Title = "OcelotConsul.ApiGateway";
                isService = false;
            }

            var pathToContentRoot = Directory.GetCurrentDirectory();
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            var host = BuildWebHost(args, pathToContentRoot);

            if (isService)
            {
                host.RunAsCustomService();
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args, string contentRoot)
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
            .UseContentRoot(contentRoot)
            .UseKestrel()
            .UseUrls("http://*:5000")
            .UseStartup<Startup>()
            .Build();
        }
    }
}
