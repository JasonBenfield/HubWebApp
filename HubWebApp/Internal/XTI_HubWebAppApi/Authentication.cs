using XTI_App.Extensions;
using XTI_TempLog;

namespace XTI_HubWebAppApi;

public sealed class Authentication
{
    private readonly TempLogSession tempLog;
    private readonly UnverifiedUser unverifiedUser;
    private readonly IAccess access;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly CachedUserContext userContext;

    internal Authentication
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

    public async Task<LoginResult> Authenticate(string userNameText, string password)
    {
        var hashedPassword = hashedPasswordFactory.Create(password);
        var userName = new AppUserName(userNameText);
        await unverifiedUser.Verify(userName, hashedPassword);
        var result =  await Authenticate(userName);
        return result;
    }

    public async Task<LoginResult> Authenticate(AppUserName userName)
    {
        var authSession = await tempLog.AuthenticateSession(userName.Value);
        var claims = new XtiClaimsCreator(authSession.SessionKey, userName).Values();
        var token = await access.GenerateToken(claims);
        userContext.ClearCache(userName);
        return new LoginResult(token);
    }
}