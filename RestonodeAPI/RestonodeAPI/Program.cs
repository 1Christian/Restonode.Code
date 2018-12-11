using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace RestonodeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddCommandLine(args);
                })
                .UseKestrel((context, options) =>
                {
                    var settings = new Restonode.Common.Settings.KestrelSettings();

                    context.Configuration.GetSection("Kestrel").Bind(settings);

                    options.Limits.MaxConcurrentConnections = settings.MaxConcurrentConnections;
                    options.Limits.MaxConcurrentUpgradedConnections = settings.MaxConcurrentUpgradedConnections;
                    options.Limits.MaxRequestBodySize = settings.MaxRequestBodySize;
                    options.Limits.MinRequestBodyDataRate =
                        new MinDataRate(bytesPerSecond: settings.MinDataRate.BytesPerSecond, gracePeriod: TimeSpan.FromSeconds(settings.MinDataRate.GracePeriod));
                    options.Limits.MinResponseDataRate =
                        new MinDataRate(bytesPerSecond: settings.MinDataRate.BytesPerSecond, gracePeriod: TimeSpan.FromSeconds(settings.MinDataRate.GracePeriod));
                    options.Listen(IPAddress.Loopback, settings.MinDataRate.ListenPort);
                })
                .UseStartup<Startup>();
    }
}