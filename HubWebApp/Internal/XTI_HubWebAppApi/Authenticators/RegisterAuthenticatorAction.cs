namespace XTI_HubWebAppApi.Authenticators;

internal sealed class RegisterAuthenticatorAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;

    public RegisterAuthenticatorAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        await app.RegisterAsAuthenticator();
        return new EmptyActionResult();
    }
}