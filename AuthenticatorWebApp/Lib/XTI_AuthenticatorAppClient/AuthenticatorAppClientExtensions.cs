// Generated Code
using Microsoft.Extensions.DependencyInjection;

namespace XTI_AuthenticatorAppClient;
public static class AuthenticatorAppClientExtensions
{
    public static void AddAuthenticatorAppClient(this IServiceCollection services)
    {
        services.TryAddScoped<IAppClientSessionKey, EmptyAppClientSessionKey>();
        services.TryAddScoped<IAppClientRequestKey, EmptyAppClientRequestKey>();
        services.AddScoped<AuthenticatorAppClientFactory>();
        services.AddScoped(sp => sp.GetRequiredService<AuthenticatorAppClientFactory>().Create());
        services.AddScoped<AuthenticatorAppClientVersion>();
    }
}