using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.Authenticators;

public sealed class RegisterUserAuthenticatorRequest
{
    public int UserID { get; set; }
    public string ExternalUserKey { get; set; } = "";
}

internal sealed class RegisterUserAuthenticatorAction : AppAction<RegisterUserAuthenticatorRequest, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly AppFactory appFactory;

    public RegisterUserAuthenticatorAction(AppFromPath appFromPath, AppFactory appFactory)
    {
        this.appFromPath = appFromPath;
        this.appFactory = appFactory;
    }

    public async Task<EmptyActionResult> Execute(RegisterUserAuthenticatorRequest model)
    {
        var app = await appFromPath.Value();
        var user = await appFactory.Users.User(model.UserID);
        await user.AddAuthenticator(app, model.ExternalUserKey);
        return new EmptyActionResult();
    }
}