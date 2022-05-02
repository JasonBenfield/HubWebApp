namespace XTI_AuthenticatorWebAppApi;

public static class AuthenticatorAppApiExtensions
{
    public static void AddAuthenticatorAppApiServices(this IServiceCollection services)
    {
        services.AddHomeGroupServices();
    }
}