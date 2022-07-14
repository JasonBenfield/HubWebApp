namespace XTI_HubWebAppApi.Auth;

public sealed class AuthenticateAction : AppAction<LoginCredentials, LoginResult>
{
    private readonly Authentication auth;

    public AuthenticateAction(Authentication auth)
    {
        this.auth = auth;
    }

    public Task<LoginResult> Execute(LoginCredentials model, CancellationToken stoppingToken) => 
        auth.Authenticate(model.UserName, model.Password);
}