using XTI_Core;

namespace XTI_HubWebAppApiActions.ExternalAuth;

public sealed class ExternalAuthKeyAction : AppAction<ExternalAuthKeyModel, AuthenticatedLoginResult>
{
    private readonly EfHubDB hubFactory;
    private readonly IClock clock;

    public ExternalAuthKeyAction(EfHubDB hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<AuthenticatedLoginResult> Execute(ExternalAuthKeyModel authRequest, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(authRequest.AuthenticatorKey);
        var efUser = await hubFactory.Users.UserOrAnonByExternalKey(authenticatorKey, authRequest.ExternalUserKey, stoppingToken);
        if (efUser.IsUserName(AppUserName.Anon))
        {
            throw new ExternalUserNotFoundException(authenticatorKey, authRequest.ExternalUserKey);
        }
        await efUser.LoggedIn("", clock.Now(), stoppingToken);
        var authID = Guid.NewGuid().ToString("N");
        var authKey = await hubFactory.StoredObjects.Store
        (
            storageName: new StorageName("XTI Authenticated"),
            generateKey: GenerateKeyModel.SixDigit(),
            data: new AuthenticatedModel(userName: efUser.ToModel().UserName, authID: authID),
            clock: clock,
            expireAfter: TimeSpan.FromMinutes(15),
            isSlidingExpiration: false,
            ct: stoppingToken
        );
        return new AuthenticatedLoginResult(AuthKey: authKey, AuthID: authID);
    }
}
