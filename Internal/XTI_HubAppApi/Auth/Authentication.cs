using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_TempLog;
using XTI_WebApp;

namespace XTI_HubAppApi.Auth;

public sealed class Authentication
{
    private readonly TempLogSession tempLog;
    private readonly UnverifiedUser unverifiedUser;
    private readonly IAccess access;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly CachedUserContext userContext;

    public Authentication
    (
        TempLogSession tempLog,
        UnverifiedUser unverifiedUser,
        IAccess access,
        IHashedPasswordFactory hashedPasswordFactory,
        CachedUserContext userContext
    )
    {
        this.tempLog = tempLog;
        this.unverifiedUser = unverifiedUser;
        this.access = access;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.userContext = userContext;
    }

    public async Task<LoginResult> Authenticate(string userName, string password)
    {
        var hashedPassword = hashedPasswordFactory.Create(password);
        var user = await unverifiedUser.Verify(new AppUserName(userName), hashedPassword);
        var authSession = await tempLog.AuthenticateSession(user.UserName().Value);
        var claims = new XtiClaimsCreator(authSession.SessionKey, user).Values();
        var token = await access.GenerateToken(claims);
        userContext.ClearCache(user.UserName());
        return new LoginResult(token);
    }
}