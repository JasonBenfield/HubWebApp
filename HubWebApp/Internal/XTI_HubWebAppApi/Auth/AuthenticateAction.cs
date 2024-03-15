namespace XTI_HubWebAppApi.Auth;

public sealed class AuthenticateAction : AppAction<AuthenticateRequest, LoginResult>
{
    private readonly Authentication auth;

    public AuthenticateAction(Authentication auth)
    {
        this.auth = auth;
    }

    public Task<LoginResult> Execute(AuthenticateRequest authRequest, CancellationToken stoppingToken) =>
        auth.Authenticate(authRequest.UserName, authRequest.Password);
}