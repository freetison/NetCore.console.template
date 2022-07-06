using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Options;
using ncapp.DependencyInjection;

namespace ncapp.Services
{
    public class HttpService: IHttpService
    {
        private readonly Uri _baseUrl;

        public HttpService(IOptions<HttpServiceOptions> options)
        {
            _baseUrl = new Uri(options.Value.BaseUrl);
        }

        public async Task<HttpResponseMessage> Ping()
        {
           var policy = ServiceExtensions
                .FlurlRetryPolicyAsync() 
                .ExecuteAsync(() => _baseUrl.GetAsync());

            var response = await _baseUrl.GetAsync();

            Console.WriteLine(response.ResponseMessage);

            return response.ResponseMessage;
        }
    }
}
