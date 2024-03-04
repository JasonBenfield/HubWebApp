namespace XTI_HubWebAppApi.Auth;

public sealed class VerifyLoginAction : AppAction<VerifyLoginForm, AuthenticatedLoginResult>
{
    private readonly UnverifiedUser unverifiedUser;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly StoredObjectFactory storedObjectFactory;

    public VerifyLoginAction(UnverifiedUser unverifiedUser, IHashedPasswordFactory hashedPasswordFactory, StoredObjectFactory storedObjectFactory)
    {
        this.unverifiedUser = unverifiedUser;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<AuthenticatedLoginResult> Execute(VerifyLoginForm model, CancellationToken stoppingToken)
    {
        var userName = model.UserName.Value() ?? "";
        var password = model.Password.Value() ?? "";
        var hashedPassword = hashedPasswordFactory.Create(password);
        await unverifiedUser.Verify(new AppUserName(userName), hashedPassword);
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