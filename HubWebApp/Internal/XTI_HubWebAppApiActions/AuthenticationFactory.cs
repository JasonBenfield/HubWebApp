using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Extensions;
using XTI_Core;
using XTI_TempLog;

namespace XTI_HubWebAppApiActions;

public sealed class AuthenticationFactory
{
    private readonly TempLogSession tempLogSession;
    private readonly HubFactory hubFactory;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly CachedUserContext cachedUserContext;
    private readonly IClock clock;
    private readonly IServiceProvider sp;

    public AuthenticationFactory(TempLogSession tempLogSession, HubFactory hubFactory, IHashedPasswordFactory hashedPasswordFactory, CachedUserContext cachedUserContext, IClock clock, IServiceProvider sp)
    {
        this.tempLogSession = tempLogSession;
        this.hubFactory = hubFactory;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.cachedUserContext = cachedUserContext;
        this.clock = clock;
        this.sp = sp;
    }

    public Authentication CreateForLogin() =>
        Create(sp.GetRequiredKeyedService<IAccess>("Login"));

    public Authentication CreateForAuthenticate() =>
        Create(sp.GetRequiredKeyedService<IAccess>("Authenticate"));

    private Authentication Create(IAccess access)
    {
        var unverifiedUser = new UnverifiedUser(hubFactory);
        return new
        (
            tempLogSession,
            unverifiedUser,
            access,
            hashedPasswordFactory,
            cachedUserContext,
            clock
        );
    }
}
