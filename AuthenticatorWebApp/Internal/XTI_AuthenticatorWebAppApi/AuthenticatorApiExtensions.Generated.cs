// Generated Code
namespace XTI_AuthenticatorWebAppApi;
public static partial class AuthenticatorApiExtensions
{
    public static void AddAuthenticatorAppApiServices(this IServiceCollection services)
    {
        services.AddHomeServices();
        services.AddScoped<AppApiFactory, AuthenticatorAppApiFactory>();
        services.AddMoreServices();
    }

    static partial void AddMoreServices(this IServiceCollection services);
}