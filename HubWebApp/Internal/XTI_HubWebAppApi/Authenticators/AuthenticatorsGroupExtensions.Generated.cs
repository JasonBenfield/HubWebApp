using XTI_HubWebAppApiActions.Authenticators;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AuthenticatorsGroupExtensions
{
    internal static void AddAuthenticatorsServices(this IServiceCollection services)
    {
        services.AddScoped<MoveAuthenticatorAction>();
        services.AddScoped<RegisterAuthenticatorAction>();
        services.AddScoped<RegisterUserAuthenticatorAction>();
        services.AddScoped<UserOrAnonByAuthenticatorAction>();
    }
}