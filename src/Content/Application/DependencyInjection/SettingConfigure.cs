namespace ncapp.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ncapp.Model;

public static class SettingConfigure
{
    public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions()
            .Configure<ApplicationInfo>(configuration.GetSection("ApplicationInfo"));
        
        services
            .AddOptions()
            .Configure<EnvironmentSettings>(configuration.GetSection("EnvironmentSettings"));

        return services;
    }
    
}
