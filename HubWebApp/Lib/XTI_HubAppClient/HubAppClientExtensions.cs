// Generated Code
using Microsoft.Extensions.DependencyInjection;

namespace XTI_HubAppClient;
public static class HubAppClientExtensions
{
    public static void AddHubAppClient(this IServiceCollection services)
    {
        services.TryAddScoped<IAppClientSessionKey, EmptyAppClientSessionKey>();
        services.TryAddScoped<IAppClientRequestKey, EmptyAppClientRequestKey>();
        services.AddScoped<HubAppClientFactory>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppClientFactory>().Create());
        services.AddScoped<HubAppClientVersion>();
    }
}