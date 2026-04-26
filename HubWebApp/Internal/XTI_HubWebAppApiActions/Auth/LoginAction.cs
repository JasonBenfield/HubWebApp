using XTI_Core;
using XTI_TempLog;

namespace XTI_HubWebAppApiActions.Auth;

public sealed class LoginAction : AppAction<AuthenticatedLoginRequest, WebRedirectResult>
{
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;
    private readonly HubWebAppOptions options;
    private readonly EfHubDB db;
    private readonly IClock clock;

    public LoginAction(AuthenticationFactory authFactory, IAnonClient anonClient, HubWebAppOptions options, EfHubDB db, IClock clock)
    {
        auth = authFactory.CreateForLogin();
        this.anonClient = anonClient;
        this.options = options;
        this.db = db;
        this.clock = clock;
    }

    public async Task<WebRedirectResult> Execute(AuthenticatedLoginRequest loginRequest, CancellationToken stoppingToken)
    {
        var authenticated = await db.StoredObjects.StoredObject<AuthenticatedModel>
        (
            new StorageName("XTI Authenticated"),
            loginRequest.AuthKey,
            clock.Now(),
            options.Storage.SingleUseExpirationInSeconds,
            stoppingToken
        );
        if (string.IsNullOrWhiteSpace(authenticated.UserName))
        {
            throw new Exception($"AuthKey '{loginRequest.AuthKey}' is not valid");
        }
        if (!authenticated.AuthID.Equals(loginRequest.AuthID, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"AuthKey auth id '{authenticated.AuthID}' does not match given auth id '{loginRequest.AuthID}'");
        }
        var userName = new AppUserName(authenticated.UserName);
        var efUser = await db.Users.UserOrAnon(userName, stoppingToken);
        await auth.Authenticate(efUser, stoppingToken);
        anonClient.Load();
        anonClient.Persist("", DateTimeOffset.MinValue, anonClient.RequesterKey);
        var loginReturn = await db.StoredObjects.StoredObject<LoginReturnModel>
        (
            new StorageName("Login Return"),
            loginRequest.ReturnKey,
            clock.Now(),
            options.Storage.SingleUseExpirationInSeconds,
            stoppingToken
        );
        var requesterKey = string.IsNullOrWhiteSpace(anonClient.RequesterKey) ?
            Guid.NewGuid().ToString("N") :
            anonClient.RequesterKey;
        if (requesterKey != loginReturn.RequesterKey)
        {
            loginReturn = new();
        }
        var returnUrl = string.IsNullOrWhiteSpace(loginReturn.ReturnUrl) ?
            options.Login.DefaultReturnUrl :
            loginReturn.ReturnUrl;
        return new WebRedirectResult(returnUrl);
    }
}