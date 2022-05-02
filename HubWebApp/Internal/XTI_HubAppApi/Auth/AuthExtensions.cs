using XTI_App.Extensions;
using XTI_Core;
using XTI_HubAppApi.Auth;
using XTI_TempLog;

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
            var hubFactory = sp.GetRequiredService<HubFactory>();
            var clock = sp.GetRequiredService<IClock>();
            return new LoginAction(auth, anonClient, hubFactory, clock);
        });
        services.AddScoped<VerifyLoginAction>();
        services.AddScoped<VerifyLoginFormAction>();
        services.AddScoped<LoginReturnKeyAction>();
    }

    private static Authentication createAuthentication(IServiceProvider sp, IAccess access)
    {
        var tempLogSession = sp.GetRequiredService<TempLogSession>();
        var unverifiedUser = new UnverifiedUser(sp.GetRequiredService<HubFactory>());
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