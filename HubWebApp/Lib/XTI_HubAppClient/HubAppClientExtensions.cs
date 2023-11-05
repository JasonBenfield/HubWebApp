// Generated Code
using Microsoft.Extensions.DependencyInjection;

namespace XTI_HubAppClient;
public static class HubAppClientExtensions
{
    public static void AddHubAppClient(this IServiceCollection services)
    {
        services.AddScoped<HubAppClient>();
        services.AddScoped<HubAppClientVersion>();
    }
}