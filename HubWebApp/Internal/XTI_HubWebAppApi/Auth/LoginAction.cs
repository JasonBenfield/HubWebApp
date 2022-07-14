using System.Text.Json;
using XTI_Core;

namespace XTI_HubWebAppApi.Auth;

internal sealed class LoginAction : AppAction<LoginModel, WebRedirectResult>
{
    private readonly Authentication auth;
    private readonly IAnonClient anonClient;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public LoginAction(Authentication auth, IAnonClient anonClient, HubFactory hubFactory, IClock clock)
    {
        this.auth = auth;
        this.anonClient = anonClient;
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<WebRedirectResult> Execute(LoginModel model, CancellationToken stoppingToken)
    {
        var serializedAuthenticated = await hubFactory.StoredObjects.StoredObject
        (
            new StorageName("XTI Authenticated"),
            model.AuthKey,
            clock.Now()
        );
        var authenticated = JsonSerializer.Deserialize<AuthenticatedModel>(serializedAuthenticated) ?? new AuthenticatedModel();
        await auth.Authenticate(new AppUserName(authenticated.UserName));
        anonClient.Load();
        anonClient.Persist("", DateTimeOffset.MinValue, anonClient.RequesterKey);
        var serializedLoginReturn = await hubFactory.StoredObjects.StoredObject(new StorageName("Login Return"), model.ReturnKey, clock.Now());
        var loginReturn = JsonSerializer.Deserialize<LoginReturnModel>(serializedLoginReturn) ?? new LoginReturnModel();
        return new WebRedirectResult(loginReturn.ReturnUrl);
    }
}