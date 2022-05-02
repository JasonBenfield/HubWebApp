using XTI_App.Extensions;
using XTI_Core;
using XTI_HubAppApi.ExternalAuth;
using XTI_TempLog;

namespace XTI_HubAppApi;

internal static class ExternalAuthExtensions
{
    public static void AddExternalAuthGroupServices(this IServiceCollection services)
    {
        services.AddScoped<ExternalAuthKeyAction>();
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