using XTI_Core;

namespace XTI_HubWebAppApi.ExternalAuth;

internal sealed class ExternalAuthKeyAction : AppAction<ExternalAuthKeyModel, AuthenticatedLoginResult>
{
    private readonly HubFactory hubFactory;
    private readonly StoredObjectFactory storedObjectFactory;
    private readonly IClock clock;

    public ExternalAuthKeyAction(HubFactory hubFactory, StoredObjectFactory storedObjectFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.storedObjectFactory = storedObjectFactory;
        this.clock = clock;
    }

    public async Task<AuthenticatedLoginResult> Execute(ExternalAuthKeyModel authRequest, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(authRequest.AuthenticatorKey);
        var user = await hubFactory.Users.UserOrAnonByExternalKey(authenticatorKey, authRequest.ExternalUserKey);
        if (user.IsUserName(AppUserName.Anon))
        {
            throw new ExternalUserNotFoundException(authenticatorKey, authRequest.ExternalUserKey);
        }
        await user.LoggedIn(clock.Now());
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName("XTI Authenticated"));
        var authID = Guid.NewGuid().ToString("N");
        var authKey = await storedObject.Store
        (
            generateKeyModel: GenerateKeyModel.SixDigit(),
            data: new AuthenticatedModel(userName: user.ToModel().UserName, authID: authID),
            expireAfter: TimeSpan.FromMinutes(15),
            isSlidingExpiration: false
        );
        return new AuthenticatedLoginResult(AuthKey: authKey, AuthID: authID);
    }
}
