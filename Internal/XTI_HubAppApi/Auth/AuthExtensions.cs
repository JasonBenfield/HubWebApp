using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Hub;
using XTI_HubAppApi.Auth;
using XTI_TempLog;
using XTI_WebApp;

namespace XTI_HubAppApi;

internal static class AuthExtensions
{
    public static void AddAuthGroupServices(this IServiceCollection services)
    {
        services.AddScoped<UnverifiedUser>();
        services.AddScoped(sp =>
        {
            var access = sp.GetRequiredService<AccessForAuthenticate>();
            var auth = createAuthentication(sp, access);
            return new AuthenticateAction(auth);
        });
        services.AddScoped(sp =>
        {
            var access = sp.GetRequiredService<AccessForLogin>();
            var auth = createAuthentication(sp, access);
            var anonClient = sp.GetRequiredService<IAnonClient>();
            return new LoginAction(auth, anonClient);
        });
        services.AddScoped<LogoutAction>();
        services.AddScoped<StartAction>();
        services.AddScoped<VerifyLoginAction>();
        services.AddScoped<VerifyLoginFormAction>();
    }

    private static Authentication createAuthentication(IServiceProvider sp, IAccess access)
    {
        var tempLogSession = sp.GetRequiredService<TempLogSession>();
        var unverifiedUser = new UnverifiedUser(sp.GetRequiredService<AppFactory>());
        var hashedPasswordFactory = sp.GetRequiredService<IHashedPasswordFactory>();
        var userContext = sp.GetRequiredService<CachedUserContext>();
        return new Authentication
        (
            tempLogSession,
            unverifiedUser,
            access,
            hashedPasswordFactory,
            userContext
        );
    }
}