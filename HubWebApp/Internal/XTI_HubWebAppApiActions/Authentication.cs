using XTI_App.Extensions;
using XTI_Core;
using XTI_TempLog;

namespace XTI_HubWebAppApiActions;

public sealed class Authentication
{
    private readonly TempLogSession tempLog;
    private readonly UnverifiedUser unverifiedUser;
    private readonly IAccess access;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly CachedUserContext userContext;
    private readonly IClock clock;

    internal Authentication
    (
        TempLogSession tempLog,
        UnverifiedUser unverifiedUser,
        IAccess access,
        IHashedPasswordFactory hashedPasswordFactory,
        CachedUserContext userContext,
        IClock clock
    )
    {
        this.tempLog = tempLog;
        this.unverifiedUser = unverifiedUser;
        this.access = access;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.userContext = userContext;
        this.clock = clock;
    }

    public async Task<LoginResult> Authenticate(string userNameText, string password, CancellationToken ct)
    {
        var hashedPassword = hashedPasswordFactory.Create(password);
        var userName = new AppUserName(userNameText);
        var efUser = await unverifiedUser.Verify(userName, hashedPassword, ct);
        var result = await Authenticate(efUser, ct);
        return result;
    }

    public async Task<LoginResult> Authenticate(EfAppUser efUser, CancellationToken ct)
    {
        var user = efUser.ToModel();
        if(user.UserName.IsBlank() || user.UserName.IsAnon())
        {
            throw new Exception($"User {user.ID} cannot be authenticated");
        }
        var authSession = await tempLog.AuthenticateSession(user.UserName.Value);
        var claims = new XtiClaimsCreator(authSession.SessionKey, user.UserName).Values();
        var token = await access.GenerateToken(claims);
        userContext.ClearCache(user.UserName);
        await efUser.LoggedIn(authSession.SessionKey.ID, clock.Now(), ct);
        return new LoginResult(token);
    }
}