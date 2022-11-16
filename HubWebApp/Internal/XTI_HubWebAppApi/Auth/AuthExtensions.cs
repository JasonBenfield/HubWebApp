using XTI_App.Extensions;
using XTI_Core;
using XTI_HubWebAppApi.Auth;
using XTI_TempLog;

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
            var hubFactory = sp.GetRequiredService<HubFactory>();
            var clock = sp.GetRequiredService<IClock>();
            return new LoginAction(auth, anonClient, hubFactory, clock);
        });
        services.AddScoped<VerifyLoginAction>();
        services.AddScoped<VerifyLoginFormAction>();
        services.AddScoped<LoginReturnKeyAction>();
    }
}