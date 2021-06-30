using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ncapp.DependencyInjection;
using RestSharp;

namespace ncapp.Services
{
    public class HttpService: IHttpService
    {
        private readonly IRestClient _restClient;

        public HttpService(IRestClient restClient, IOptions<HttpServiceOptions> options)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(options.Value.BaseUrl);
        }

        public async Task<string> Ping(Uri uri = null)
        {
            if (uri != null) { _restClient.BaseUrl = uri;}

            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = await _restClient.ExecuteAsync(request);

            Console.WriteLine(response.Content);

            return response.Content;
        }
    }
}
