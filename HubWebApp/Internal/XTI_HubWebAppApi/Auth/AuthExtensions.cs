using XTI_HubWebAppApi.Auth;

namespace XTI_HubWebAppApi;

internal static class AuthExtensions
{
    public static void AddAuthGroupServices(this IServiceCollection services)
    {
        services.AddScoped<UnverifiedUser>();
        services.AddScoped(sp =>
        {
            var auth = sp.GetRequiredService<AuthenticationFactory>().CreateForAuthenticate();
            return new AuthenticateAction(auth);
        });
        services.AddScoped(sp =>
        {
            var auth = sp.GetRequiredService<AuthenticationFactory>().CreateForLogin();
            var anonClient = sp.GetRequiredService<IAnonClient>();
            var storedObjectDB = sp.GetRequiredService<IStoredObjectDB>();
            var options = sp.GetRequiredService<HubWebAppOptions>();
            return new LoginAction(auth, anonClient, options, storedObjectDB);
        });
        services.AddScoped<VerifyLoginAction>();
        services.AddScoped<VerifyLoginFormAction>();
        services.AddScoped<LoginReturnKeyAction>();
        services.AddScoped<LoginValidation>();
    }
}