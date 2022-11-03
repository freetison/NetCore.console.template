using Flurl.Http;
using Flurl.Http.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ncapp.Model;

using Serilog;

namespace ncapp.DependencyInjection;

public static class HttpConfigure
{
    public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var envSettings = configuration.Get<EnvironmentSettings>();

        services.AddSingleton<IFlurlClientFactory>(_ =>
            new PerBaseUrlFlurlClientFactory()
                .ConfigureClient(envSettings.ServiceUrl, cli =>
                    {
                        cli.AllowAnyHttpStatus();
                        cli.Configure(settings =>
                        {
                            settings.BeforeCall = call => Log.Information($"Calling {call.Request.Url}");
                            settings.OnError = call =>
                                Log.Error($"Call to {call.Request.Url} failed: {call.Exception}");
                        }).WithHeaders(new
                        {
                            Accept = "application/json",
                        });

                    })
            );

        return services;
    }
}