using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.Authenticators;

namespace XTI_HubAppApi;

internal static class AuthenticatorsExtensions
{
    public static void AddAuthenticatorsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<RegisterAuthenticatorAction>();
        services.AddScoped<RegisterUserAuthenticatorAction>();
    }
}