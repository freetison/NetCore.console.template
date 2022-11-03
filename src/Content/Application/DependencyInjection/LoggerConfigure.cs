using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ncapp.DependencyInjection;

public static class LoggerConfigure
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddSerilog(dispose: true);
            loggingBuilder.AddDebug();
        });
        return services;
    }

}