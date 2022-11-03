using System;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Options;
using ncapp.Model;

using Polly;
using Polly.Retry;

namespace ncapp.Services
{
    public class BoredApiService: IBoredApiService
    {
        private readonly EnvironmentSettings _environmentSettings;

        public BoredApiService(IOptions<EnvironmentSettings> environmentSettings)
        {
            _environmentSettings = environmentSettings.Value;
        }

        public async Task<string> GetData()
        {
            var response = await RetryPolicy()
                .ExecuteAsync(() => 
                    _environmentSettings.ServiceUrl
                    .AllowAnyHttpStatus()
                    .GetStringAsync()
                );

            return response;
        }

        public static AsyncRetryPolicy RetryPolicy()
        {
            return Policy
                .Handle<FlurlHttpException>(resp =>
                    resp.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    resp.StatusCode == (int)HttpStatusCode.NotFound ||
                    resp.StatusCode == (int)HttpStatusCode.RequestTimeout ||
                    resp.StatusCode == (int)HttpStatusCode.BadGateway
                )
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (delegateResult, retryCount) =>
                    {
                        Console.WriteLine($"Retrying, attempt {retryCount}");
                    });
        }
    }
    
}
