using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

internal sealed class LoginAction : AppAction<AuthenticatedLoginRequest, WebRedirectResult>
{
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;
    private readonly LoginOptions options;

    public LoginAction(Authentication auth, IAnonClient anonClient, HubFactory hubFactory, IClock clock, LoginOptions options)
    {
        this.auth = auth;
        this.anonClient = anonClient;
        this.hubFactory = hubFactory;
        this.clock = clock;
        this.options = options;
    }

    public async Task<WebRedirectResult> Execute(AuthenticatedLoginRequest model, CancellationToken stoppingToken)
    {
        var serializedAuthenticated = await hubFactory.StoredObjects.StoredObject
        (
            new StorageName("XTI Authenticated"),
            model.AuthKey,
            clock.Now()
        );
        var authenticated = string.IsNullOrWhiteSpace(serializedAuthenticated) ?
            new() :
            XtiSerializer.Deserialize<AuthenticatedModel>(serializedAuthenticated);
        if (string.IsNullOrWhiteSpace(authenticated.UserName))
        {
            throw new Exception($"AuthKey '{model.AuthKey}' is not valid");
        }
        if (!authenticated.AuthID.Equals(model.AuthID, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"AuthKey auth id '{authenticated.AuthID}' does not match given auth id '{model.AuthID}'");
        }
        await auth.Authenticate(new AppUserName(authenticated.UserName));
        anonClient.Load();
        anonClient.Persist("", DateTimeOffset.MinValue, anonClient.RequesterKey);
        var serializedLoginReturn = await hubFactory.StoredObjects.StoredObject(new StorageName("Login Return"), model.ReturnKey, clock.Now());
        var loginReturn = string.IsNullOrWhiteSpace(serializedLoginReturn) ?
            new() :
            XtiSerializer.Deserialize<LoginReturnModel>(serializedLoginReturn);
        var returnUrl = string.IsNullOrWhiteSpace(loginReturn.ReturnUrl) ?
            options.DefaultReturnUrl :
            loginReturn.ReturnUrl;
        return new WebRedirectResult(returnUrl);
    }
}