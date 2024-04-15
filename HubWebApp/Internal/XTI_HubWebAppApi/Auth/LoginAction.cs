using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

internal sealed class LoginAction : AppAction<AuthenticatedLoginRequest, WebRedirectResult>
{
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;
    private readonly HubWebAppOptions options;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public LoginAction(Authentication auth, IAnonClient anonClient, HubWebAppOptions options, HubFactory hubFactory, IClock clock)
    {
        this.auth = auth;
        this.anonClient = anonClient;
        this.options = options;
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<WebRedirectResult> Execute(AuthenticatedLoginRequest loginRequest, CancellationToken stoppingToken)
    {
        var authenticated = await hubFactory.StoredObjects.StoredObject<AuthenticatedModel>
        (
            new StorageName("XTI Authenticated"),
            loginRequest.AuthKey,
            clock.Now()
        );
        if (string.IsNullOrWhiteSpace(authenticated.UserName))
        {
            throw new Exception($"AuthKey '{loginRequest.AuthKey}' is not valid");
        }
        if (!authenticated.AuthID.Equals(loginRequest.AuthID, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"AuthKey auth id '{authenticated.AuthID}' does not match given auth id '{loginRequest.AuthID}'");
        }
        await auth.Authenticate(new AppUserName(authenticated.UserName));
        anonClient.Load();
        anonClient.Persist("", DateTimeOffset.MinValue, anonClient.RequesterKey);
        var loginReturn = await hubFactory.StoredObjects.StoredObject<LoginReturnModel>
        (
            new StorageName("Login Return"), 
            loginRequest.ReturnKey,
            clock.Now()
        );
        var returnUrl = string.IsNullOrWhiteSpace(loginReturn.ReturnUrl) ?
            options.Login.DefaultReturnUrl :
            loginReturn.ReturnUrl;
        return new WebRedirectResult(returnUrl);
    }
}