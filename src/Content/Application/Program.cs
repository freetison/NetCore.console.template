using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ncapp;
using ncapp.DependencyInjection;
using ncapp.Services;

using System;

var host = CreateHostBuilder(args).Build();
App app = host.Services.GetRequiredService<App>();
if (app != null) await app.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;

            config.AddEnvironmentVariables();
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddLogger();
            services.ConfigureSettings(hostContext.Configuration);
            services.AddHttpClient(hostContext.Configuration);
            services.AddTransient<IBoredApiService, BoredApiService>();
            services.AddSingleton<App>();
            
        });


