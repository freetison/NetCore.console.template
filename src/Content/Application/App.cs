using System;
using System.Threading.Tasks;
using ncapp.Services;
using Serilog;

namespace ncapp
{
    public class App
    {
        private readonly IBoredApiService _boredApiService;
        
        public App(IBoredApiService boredApiService)
        {
            _boredApiService = boredApiService;
        }

        public async Task Run()
        {
            Log.Information("App started !!!");
            var result = await _boredApiService.GetData();
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
