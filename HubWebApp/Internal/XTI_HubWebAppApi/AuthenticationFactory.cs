using XTI_App.Extensions;
using XTI_TempLog;

namespace XTI_HubWebAppApi;

public sealed class AuthenticationFactory
{
    private readonly TempLogSession tempLogSession;
    private readonly HubFactory hubFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly CachedUserContext cachedUserContext;
    private readonly IServiceProvider sp;

    public AuthenticationFactory(TempLogSession tempLogSession, HubFactory hubFactory, IHashedPasswordFactory hashedPasswordFactory, CachedUserContext cachedUserContext, IServiceProvider sp)
    {
        this.tempLogSession = tempLogSession;
        this.hubFactory = hubFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.cachedUserContext = cachedUserContext;
        this.sp = sp;
    }

    public Authentication CreateForLogin() =>
        Create(sp.GetRequiredService<AccessForLogin>());

    public Authentication CreateForAuthenticate() =>
        Create(sp.GetRequiredService<AccessForAuthenticate>());

    private Authentication Create(IAccess access)
    {
        var unverifiedUser = new UnverifiedUser(hubFactory);
        return new Authentication
        (
            tempLogSession,
            unverifiedUser,
            access,
            hashedPasswordFactory,
            cachedUserContext
        );
    }
}
