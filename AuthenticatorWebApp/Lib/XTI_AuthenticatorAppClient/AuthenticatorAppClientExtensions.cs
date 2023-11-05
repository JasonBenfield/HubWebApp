// Generated Code
using Microsoft.Extensions.DependencyInjection;

namespace XTI_AuthenticatorAppClient;
public static class AuthenticatorAppClientExtensions
{
    public static void AddAuthenticatorAppClient(this IServiceCollection services)
    {
        services.AddScoped<AuthenticatorAppClient>();
        services.AddScoped<AuthenticatorAppClientVersion>();
    }
}