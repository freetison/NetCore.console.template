using System;
using System.Net;
using System.Net.Http;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ncapp.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog;

namespace ncapp.DependencyInjection
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddHttpService(this IServiceCollection services, Action<HttpServiceOptions> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            services.Configure(setupAction);
            services.AddSingleton<HttpServiceOptions, HttpServiceOptions>();
            
            services.AddScoped<IHttpService, HttpService>();

            services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
            FlurlHttp.ConfigureClient("https://google.com", cli => cli.Configure(settings =>
                {
                    settings.BeforeCall = call => Log.Information($"Send request to: {call.Request.Client.BaseUrl}");
                    settings.OnError = call => Log.Information($"request failed {call.Exception.Message}"); 
                    settings.ConnectionLeaseTimeout = TimeSpan.FromSeconds(5);
                    settings.HttpClientFactory = new UntrustedCertClientFactory();
                })
                .WithHeader("Accept", "application/json"));

            return services;
        }

        public static AsyncRetryPolicy<HttpResponseMessage> HttpRetryPolicyAsync()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .OrResult(msg => msg.StatusCode == HttpStatusCode.Unauthorized)
                .OrResult(msg => msg.StatusCode == HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static AsyncRetryPolicy<IFlurlResponse> FlurlRetryPolicyAsync()
        {
            return Policy
                .HandleResult<IFlurlResponse>(r =>
                    r.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    r.StatusCode == (int)HttpStatusCode.NotFound ||
                    r.StatusCode == (int)HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
    }

}
