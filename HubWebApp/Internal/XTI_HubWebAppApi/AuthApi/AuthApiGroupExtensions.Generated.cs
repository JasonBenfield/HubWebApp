using XTI_HubWebAppApiActions.AuthApi;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AuthApiGroupExtensions
{
    internal static void AddAuthApiServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticateAction>();
        services.AddScoped<AuthenticateValidation>();
    }
}