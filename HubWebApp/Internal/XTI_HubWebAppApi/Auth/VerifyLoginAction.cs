using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

public sealed class VerifyLoginAction : AppAction<VerifyLoginForm, AuthenticatedLoginResult>
{
    private readonly UnverifiedUser unverifiedUser;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly StoredObjectFactory storedObjectFactory;
    private readonly IClock clock;

    public VerifyLoginAction(UnverifiedUser unverifiedUser, IHashedPasswordFactory hashedPasswordFactory, StoredObjectFactory storedObjectFactory, IClock clock)
    {
        this.unverifiedUser = unverifiedUser;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.storedObjectFactory = storedObjectFactory;
        this.clock = clock;
    }

    public async Task<AuthenticatedLoginResult> Execute(VerifyLoginForm loginForm, CancellationToken stoppingToken)
    {
        var userName = loginForm.UserName.Value() ?? "";
        var password = loginForm.Password.Value() ?? "";
        var hashedPassword = hashedPasswordFactory.Create(password);
        var user = await unverifiedUser.Verify(new AppUserName(userName), hashedPassword);
        await user.LoggedIn(clock.Now());
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName("XTI Authenticated"));
        var authID = Guid.NewGuid().ToString("N");
        var authKey = await storedObject.Store
        (
            generateKeyModel: GenerateKeyModel.SixDigit(),
            data: new AuthenticatedModel(userName: new AppUserName(userName), authID: authID),
            expireAfter: TimeSpan.FromMinutes(15),
            isSlidingExpiration: false
        );
        return new AuthenticatedLoginResult(AuthKey: authKey, AuthID: authID);
    }
}