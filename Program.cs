﻿using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Fivet.Dao;
using Fivet.ZeroIce;
using Fivet.ZeroIce.model;


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
                //TheSystem
                services.AddSingleton<TheSystemDisp_,TheSystemImpl>();
                //Contratos
                services.AddSingleton<ContratosDisp_,ContratosImpl>();
                //FiveContext
                services.AddDbContext<FivetContext>();
                // The FivetService 
                services.AddHostedService<FivetService>();
                services.AddLogging();
                services.Configure<HostOptions>(options =>{
                    options.ShutdownTimeout = System.TimeSpan.FromSeconds(15);
            } );
        });
    }
}
