using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Hub;
using XTI_HubAppApi.ExternalAuth;
using XTI_TempLog;
using XTI_WebApp;

namespace XTI_HubAppApi;

internal static class ExternalAuthExtensions
{
    public static void AddExternalAuthGroupServices(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            var appFromPath = sp.GetRequiredService<AppFromPath>();
            var appFactory = sp.GetRequiredService<AppFactory>();
            var access = sp.GetRequiredService<AccessForLogin>();
            var auth = createAuthentication(sp, access);
            var anonClient = sp.GetRequiredService<IAnonClient>();
            return new ExternalLoginAction(appFromPath, appFactory, auth, anonClient);
        });
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