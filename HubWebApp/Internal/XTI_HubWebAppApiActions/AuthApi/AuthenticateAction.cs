namespace XTI_HubWebAppApiActions.AuthApi;

public sealed class AuthenticateAction : AppAction<AuthenticateRequest, LoginResult>
{
    private readonly Authentication auth;

    public AuthenticateAction(AuthenticationFactory authFactory)
    {
        auth = authFactory.CreateForAuthenticate();
    }

    public Task<LoginResult> Execute(AuthenticateRequest authRequest, CancellationToken stoppingToken) =>
        auth.Authenticate(authRequest.UserName, authRequest.Password);
}