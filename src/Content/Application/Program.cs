namespace ncapp
{
    using System;
    using Serilog;
    using System.IO;
    using System.Threading.Tasks;
    using DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        public static IConfigurationRoot Configuration;

        static int Main(string[] args)
        {
            try
            {
                // Start!
                MainAsync(args).Wait();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                // Create service collection
                ServiceCollection serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                // Create service provider
                IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

                await serviceProvider.GetService<App>()?.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error running app");
                throw ex;
            }
            finally
            {
                Log.CloseAndFlush();
                Console.ReadKey();
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add logging
            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddSerilog(dispose: true);
                loggingBuilder.AddDebug();
            });

            // Build configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile("appsettings.json", true)
                .Build();

            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();

            // Add options
            serviceCollection.AddOptions();
            serviceCollection.AddSingleton(Configuration);

            // Add client
            serviceCollection.AddHttpService(options =>
            {
                options.BaseUrl = "https://google.com";
                //options.Authenticator = new HttpBasicAuthenticator("user", "xxxxxxxx");
            });


            // Add app
            serviceCollection.AddSingleton<App>();
        }
    }
}
