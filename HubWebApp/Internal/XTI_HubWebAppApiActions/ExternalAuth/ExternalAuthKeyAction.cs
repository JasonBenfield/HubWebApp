using XTI_Core;

namespace XTI_HubWebAppApiActions.ExternalAuth;

public sealed class ExternalAuthKeyAction : AppAction<ExternalAuthKeyModel, AuthenticatedLoginResult>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public ExternalAuthKeyAction(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
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
        var authID = Guid.NewGuid().ToString("N");
        var authKey = await hubFactory.StoredObjects.Store
        (
            storageName: new StorageName("XTI Authenticated"),
            generateKey: GenerateKeyModel.SixDigit(),
            data: new AuthenticatedModel(userName: user.ToModel().UserName, authID: authID),
            clock: clock,
            expireAfter: TimeSpan.FromMinutes(15),
            isSlidingExpiration: false
        );
        return new AuthenticatedLoginResult(AuthKey: authKey, AuthID: authID);
    }
}
