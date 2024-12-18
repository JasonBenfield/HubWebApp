using XTI_HubWebAppApiActions.Auth;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AuthGroupExtensions
{
    internal static void AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<LoginAction>();
        services.AddScoped<LoginValidation>();
        services.AddScoped<LoginReturnKeyAction>();
        services.AddScoped<VerifyLoginAction>();
        services.AddScoped<VerifyLoginFormAction>();
    }
}