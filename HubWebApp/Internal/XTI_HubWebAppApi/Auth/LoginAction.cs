using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

internal sealed class LoginAction : AppAction<AuthenticatedLoginRequest, WebRedirectResult>
{
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;
    private readonly HubWebAppOptions options;
    private readonly IStoredObjectDB storedObjectDB;

    public LoginAction(Authentication auth, IAnonClient anonClient, HubWebAppOptions options, IStoredObjectDB storedObjectDB)
    {
        this.auth = auth;
        this.anonClient = anonClient;
        this.options = options;
        this.storedObjectDB = storedObjectDB;
    }

    public async Task<WebRedirectResult> Execute(AuthenticatedLoginRequest loginRequest, CancellationToken stoppingToken)
    {
        var serializedAuthenticated = await storedObjectDB.Value
        (
            new StorageName("XTI Authenticated"),
            loginRequest.AuthKey
        );
        var authenticated = string.IsNullOrWhiteSpace(serializedAuthenticated) ?
            new() :
            XtiSerializer.Deserialize<AuthenticatedModel>(serializedAuthenticated);
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
        var serializedLoginReturn = await storedObjectDB.Value(new StorageName("Login Return"), loginRequest.ReturnKey);
        var loginReturn = string.IsNullOrWhiteSpace(serializedLoginReturn) ?
            new() :
            XtiSerializer.Deserialize<LoginReturnModel>(serializedLoginReturn);
        var returnUrl = string.IsNullOrWhiteSpace(loginReturn.ReturnUrl) ?
            options.Login.DefaultReturnUrl :
            loginReturn.ReturnUrl;
        return new WebRedirectResult(returnUrl);
    }
}