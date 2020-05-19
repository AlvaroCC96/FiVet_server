using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Fivet.Server
{
    class Program
    {
         static void Main(string[] args)
        {   
            
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args ) => 
        Host
        .CreateDefaultBuilder(args)
        .UseEnvironment("Development")
        .ConfigureLogging(loggin=> {
            loggin.ClearProviders();
            loggin.AddConsole(options => {
                    options.IncludeScopes = true;
                    options.DisableColors = false;
                    options.TimestampFormat = "[yyyyMMdd.HHmmss.fff]";
                });
            loggin.SetMinimumLevel(LogLevel.Trace);
            })
            .UseConsoleLifetime()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<FivetService>();
                services.AddLogging();
                services.Configure<HostOptions>(options =>{
                    options.ShutdownTimeout = System.TimeSpan.FromSeconds(15);
            } );
        });
    }
}
