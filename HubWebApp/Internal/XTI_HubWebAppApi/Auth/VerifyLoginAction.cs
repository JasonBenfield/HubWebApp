namespace XTI_HubWebAppApi.Auth;

public sealed class VerifyLoginAction : AppAction<VerifyLoginForm, string>
{
    private readonly UnverifiedUser unverifiedUser;
    private readonly IHashedPasswordFactory hashedPasswordFactory;
    private readonly HubFactory hubFactory;
    private readonly StoredObjectFactory storedObjectFactory;

    public VerifyLoginAction(UnverifiedUser unverifiedUser, IHashedPasswordFactory hashedPasswordFactory, HubFactory hubFactory, StoredObjectFactory storedObjectFactory)
    {
        this.unverifiedUser = unverifiedUser;
        this.hashedPasswordFactory = hashedPasswordFactory;
        this.hubFactory = hubFactory;
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<string> Execute(VerifyLoginForm model, CancellationToken stoppingToken)
    {
        var userName = model.UserName.Value() ?? "";
        var password = model.Password.Value() ?? "";
        var hashedPassword = hashedPasswordFactory.Create(password);
        await unverifiedUser.Verify(new AppUserName(userName), hashedPassword);
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName("XTI Authenticated"));
        var authKey = await storedObject.Store
        (
            GeneratedStorageKeyType.Values.SixDigit,
            new AuthenticatedModel {  UserName = userName },
            TimeSpan.FromMinutes(30)
        );
        return authKey;
    }
}