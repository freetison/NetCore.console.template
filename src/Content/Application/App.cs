using System;
using System.Threading.Tasks;
using ncapp.Services;
using Serilog;

namespace ncapp
{
    public class App
    {
        private readonly IHttpService _httpService;


        public App(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Run()
        {
            Log.Information("App started !!!");
            Console.WriteLine(await _httpService.Ping(new Uri("https://www.boredapi.com/api/activity")));

        }
    }
}
