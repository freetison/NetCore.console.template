using System;
using Microsoft.Extensions.DependencyInjection;
using ncapp.Services;
using RestSharp;

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
            services.AddTransient<IRestClient, RestClient>();
            services.AddScoped<IHttpService, HttpService>();
            return services;
        }

    }

}
